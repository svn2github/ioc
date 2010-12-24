// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using System.IO;

namespace OpenNETCF.IoC
{
    public interface IModuleInfo
    {
        string AssemblyFile { get; }
        Assembly Assembly { get; }
    }

    public class ModuleInfo : IModuleInfo
    {
        public string AssemblyFile { get; internal set; }
        public Assembly Assembly { get; internal set; }
    }

    public sealed class ModuleInfoStoreService
    {
        public event EventHandler<GenericEventArgs<string>> ModuleLoaded;

        List<IModuleInfo> m_loadedModules = new List<IModuleInfo>();

        internal ModuleInfoStoreService()
        {
        }

        internal void LoadModulesFromStore(IModuleInfoStore store)
        {
            Guard.ArgumentNotNull(store, "store");

            string xml = store.GetModuleListXml();
            if (xml == null) return;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            XElement root = XElement.Parse(xml);

            string s = "Modules";
            var modules = from n in root.Descendants(s)
                          select n;

            var assemblies = from n in modules.Descendants()
                             where n.Name == "ModuleInfo"
                             select n.Attribute("AssemblyFile").Value;


            LoadAssemblies(assemblies);
        }

        internal void LoadAssembly(Assembly assembly)
        {
            var imodule = (from t in assembly.GetTypes()
                           where t.GetInterfaces().Count(i => i.Equals(typeof(IModule))) > 0
                           select t).FirstOrDefault();

            if (imodule == null) return;

            object instance = ObjectFactory.CreateObject(imodule, RootWorkItem.Instance);

            var assemblyName = assembly.GetName();

            m_loadedModules.Add(
                new ModuleInfo 
                { 
                    Assembly = assembly,
                    AssemblyFile = assemblyName.CodeBase 
                });

            var loadMethod = imodule.GetMethod("Load", BindingFlags.Public | BindingFlags.Instance);
            if (loadMethod != null)
            {
                try
                {
                    loadMethod.Invoke(instance, null);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }

            var addServices = imodule.GetMethod("AddServices", BindingFlags.Public | BindingFlags.Instance);
            if (addServices != null)
            {
                try
                {
                    addServices.Invoke(instance, null);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }

            RaiseModuleLoaded(assemblyName.Name);
        }

        private void RaiseModuleLoaded(string moduleName)
        {
            EventHandler<GenericEventArgs<string>> handler = ModuleLoaded;
            if (handler == null) return;

            handler(this, new GenericEventArgs<string>(moduleName));
        }

        private void LoadAssemblies(IEnumerable<string> assemblyNames)
        {
            Guard.ArgumentNotNull(assemblyNames, "assemblyNames");

            string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            Uri pathasURI = new Uri(rootFolder);

            Assembly asm;

            foreach (var s in assemblyNames)
            {
                asm = null;

                FileInfo fi = new FileInfo(Path.Combine(pathasURI.LocalPath, s));

                if (fi.Exists)
                {
                    // local?
                    asm = Assembly.LoadFrom(fi.FullName);
                }
                else if (File.Exists(s))
                {
                    // fully qualified path?
                    asm = Assembly.LoadFrom(s);
                }
                else if (File.Exists(Path.Combine("\\Windows", s)))
                {
                    // Windows?
                    asm = Assembly.LoadFrom(Path.Combine("\\Windows", s));
                }
                else
                {
                    throw new IOException(string.Format("Unable to locate assembly '{0}'", s));
                }

                if (asm == null) continue;

                LoadAssembly(asm);
            }
        }

        public IModuleInfo[] LoadedModules
        {
            get { return m_loadedModules.ToArray(); }
        }
    }
}
