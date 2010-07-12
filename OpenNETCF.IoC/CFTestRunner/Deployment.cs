using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Diagnostics;

namespace OpenNETCF.MsTest
{
    public class Deployment
    {
        public List<string> PassedTests { get; set; }
        public List<string> FailedTests { get; set; }
        public List<string> IgnoredTests { get; set; }
        public Exception RunError { get; set; }
        public ITestReporter Reporter { get; set; }

        private static Deployment m_instance = null;

        // tests that don't finish in this period of time are considered failures
        private const int TEST_TIMEOUT_MILLISECONDS = 100000;
 
        private Deployment()
        {
            PassedTests = new List<string>();
            FailedTests = new List<string>();
            IgnoredTests = new List<string>();

            // set our default reporter
            Reporter = new DebugWindowReporter();
        }

        public static Deployment Manager
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new Deployment();
                }

                return m_instance;
            }
        }

        public static DeployParameters TranslateArgs(string[] args)
        {
            // for now we just assume any args are assembly names.  This will change in the future when I add actual args
            DeployParameters @params = new DeployParameters();

            List<string> assemblies = new List<string>();

            foreach (var arg in args)
            {
                // TODO: see if the file actually exists
                assemblies.Add(arg);
            }

            @params.TestAssemblies = assemblies.ToArray();

            return @params;
        }

        public void Deploy()
        {
            Deploy(new DeployParameters(GetCurrentDirectory()));
        }

        public void Deploy(DeployParameters parameters)
        {
            Assembly a = Assembly.Load("Microsoft.VisualStudio.SmartDevice.UnitTestFramework");
            Type t = a.GetType("Microsoft.VisualStudio.TestTools.UnitTesting.UTFDeviceSharedData", true);
            FieldInfo f = t.GetField("s_sharedAssemblyOutPath", BindingFlags.Static | BindingFlags.NonPublic);

            if (parameters.TestAssemblyFolder == null)
            {
                parameters.TestAssemblyFolder = GetCurrentDirectory();
            }

            f.SetValue(null, parameters.TestAssemblyFolder);

            string[] testList;
            if ((parameters.TestAssemblies == null) || (parameters.TestAssemblies.Length == 0))
            {
                testList = Directory.GetFiles(parameters.TestAssemblyFolder, "*.dll");
            }
            else
            {
                testList = parameters.TestAssemblies;
            }

            foreach (string filename in testList)
            {
                if (!RunTests(Path.GetFileNameWithoutExtension(filename)))
                {
                    break;
                }
            }
        }

        public void Report()
        {
            if (Reporter != null)
            {
                Reporter.RunReport(this);
            }
        }

        private string GetCurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        private bool RunTests(string assembly)
        {
            Assembly a;
            try
            {
                a = Assembly.Load(assembly);
            }
            catch (IOException ex)
            {
                RunError = new TestRunException(
                    string.Format("** Unable to load test assembly '{0}'.  Ensure it, and all of its dependencies, are in the same output folder as the TestRunner", assembly),
                    ex);
                return false;
            }
            try
            {
                var types = GetTestTypes(a);
                foreach (var t in types)
                {
                    RunTestClass(t);
                }
            }
            catch (TestRunException ex)
            {
                RunError = ex;

                return false;
            }
            catch (Exception ex)
            {
                RunError = new TestRunException(
                    "Test Run failed",
                    ex);

                return false;
            }

            return true;
        }

        private void RunTestClass(Type t)
        {
            var testMethods = GetTestMethods(t);

            // construct the test class
            object testInstance = t.GetConstructor(new Type[] { }).Invoke(null);

            foreach (var m in testMethods)
            {
                Type expectedExceptionType = null;

                try
                {
                    // see if an exception is expected
                    var attrib = m.GetCustomAttributes(typeof(ExpectedExceptionAttribute), true).FirstOrDefault() as ExpectedExceptionAttribute;
                    if (attrib != null)
                    {
                        expectedExceptionType = attrib.ExceptionType;
                    }

                    // run Testinitialize
                    RunAttributedMethod(testInstance, typeof(TestInitializeAttribute));

                    Exception threadException = null;

                    // run the test
                    Thread testThread = new Thread(delegate
                        {
                            try
                            {
                                m.Invoke(testInstance, null);
                            }
                            catch (TargetInvocationException ex)
                            {
                                threadException = ex;
                            }
                        }) 
                        { IsBackground = true };
                    testThread.Start();
                    if (!testThread.Join(TEST_TIMEOUT_MILLISECONDS))
                    {
                        try
                        {
                            testThread.Abort();
                        }
                        catch (ThreadAbortException)
                        {
                        }
                        throw new TimeoutException(string.Format("Test '{0}' failed to complete in {1}ms", m.Name, TEST_TIMEOUT_MILLISECONDS));
                    }

                    if (threadException != null)
                    {
                        throw threadException;
                    }

                    // Run test cleanup
                    RunAttributedMethod(testInstance, typeof(TestCleanupAttribute));

                    PassedTests.Add(m.Name);
                }
                catch (Exception ex)
                {
                    if ((expectedExceptionType != null) && (expectedExceptionType.Equals(ex.InnerException.GetType())))
                    {
                        PassedTests.Add(m.Name);
                    }
                    else
                    {
                        if (ex.InnerException != null)
                        {
                            FailedTests.Add(string.Format("{0} [{1}]", m.Name, ex.InnerException.Message));
                        }
                        else
                        {
                            FailedTests.Add(string.Format("{0} [{1}]", m.Name, ex.Message));
                        }
                    }
                }
            }
        }

        private void RunAttributedMethod(object testInstance, Type attributeType)
        {
            foreach (var m in from method in testInstance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                              where method.GetCustomAttributes(attributeType, true).Length > 0
                              select method)
            {
                m.Invoke(testInstance, null);
            }
        }

        private List<MethodInfo> GetTestMethods(Type t)
        {
            var methods = new List<MethodInfo>();

            foreach (var m in t.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (m.GetCustomAttributes(typeof(TestMethodAttribute), true).Length > 0)
                {
                    if (m.GetCustomAttributes(typeof(IgnoreAttribute), true).Length > 0)
                    {
                        IgnoredTests.Add(m.Name);
                    }
                    else
                    {
                        methods.Add(m);
                    }
                }
            }
            return methods;
        }

        private List<Type> GetTestTypes(Assembly assembly)
        {
            var types = new List<Type>();
            try
            {
                foreach (Type t in assembly.GetTypes())
                {
                    object[] o = t.GetCustomAttributes(typeof(TestClassAttribute), true);
                    if (o.Length > 0)
                    {
                        types.Add(t);
                    }
                }
            }
            catch (TypeLoadException ex)
            {
                throw new TestRunException(
                    "Missing Type Information.  Check that you have deployed all dependencies.",
                    ex);
            }

            return types;
        }
    }
}
