using System.Diagnostics;

namespace OpenNETCF.MsTest
{
    public class DebugWindowReporter : ITestReporter
    {
        public void RunReport(Deployment deployment)
        {
            // see if the deployment failed
            if (deployment.RunError != null)
            {
                Debug.WriteLine("\r\n**** UNABLE TO RUN TESTS ****");
                Debug.WriteLine(deployment.RunError.Message);
                if (deployment.RunError.InnerException != null)
                {
                    Debug.WriteLine(string.Format("{0}: {1}",
                        deployment.RunError.InnerException.GetType().Name,
                        deployment.RunError.InnerException.Message));
                }

                Debug.WriteLine("\r\n*************************");
                return;
            }

            // report the test results
            Debug.WriteLine("\r\n*************************");
            if (deployment.PassedTests.Count > 0)
            {
                Debug.WriteLine("PASSED:");
                foreach (var i in deployment.PassedTests)
                {
                    Debug.WriteLine(string.Format("\t{0}", i));
                }
            }
            if (deployment.FailedTests.Count > 0)
            {
                Debug.WriteLine("FAILED:");
                foreach (var i in deployment.FailedTests)
                {
                    Debug.WriteLine(string.Format("\t{0}", i));
                }
            }

            Debug.WriteLine("-------------------------");
            Debug.WriteLine(string.Format("*** {0} tests passed.", deployment.PassedTests.Count));
            Debug.WriteLine(string.Format("*** {0} tests failed.", deployment.FailedTests.Count));
            Debug.WriteLine(string.Format("*** {0} tests ignored.", deployment.IgnoredTests.Count));
            Debug.WriteLine("*************************\r\n");
        }
    }
}
