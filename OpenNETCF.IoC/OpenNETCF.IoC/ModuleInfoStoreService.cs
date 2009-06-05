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
    }

    public class ModuleInfo : IModuleInfo
    {
        public string AssemblyFile { get; internal set; }
    }

    public sealed class ModuleInfoStoreService
    {
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

        private void LoadAssemblies(IEnumerable<string> assemblyNames)
        {
            Guard.ArgumentNotNull(assemblyNames, "assemblyNames");

            string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            Assembly asm;

            foreach (var s in assemblyNames)
            {
                asm = null;
                FileInfo fi = new FileInfo(Path.Combine(rootFolder, s));

                if(fi.Exists)
                {
                    // local?
                    asm = Assembly.LoadFrom(fi.FullName);
                }
                else if(File.Exists(s))
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

                if(asm == null) continue;

                var imodule = (from t in asm.GetTypes()
                               where t.GetInterfaces().Count(i => i.Equals(typeof(IModule))) > 0
                               select t).FirstOrDefault();

                if (imodule == null) continue;

                object instance = Activator.CreateInstance(imodule);

                m_loadedModules.Add(new ModuleInfo { AssemblyFile = asm.GetName().CodeBase });

                var loadMethod = imodule.GetMethod("Load", BindingFlags.Public | BindingFlags.Instance);
                if (loadMethod != null)
                {
                    loadMethod.Invoke(instance, null);
                }

                var addServices = imodule.GetMethod("AddServices", BindingFlags.Public | BindingFlags.Instance);
                if (addServices != null)
                {
                    addServices.Invoke(instance, null);
                }                      
            }
        }

        public IModuleInfo[] LoadedModules
        {
            get { return m_loadedModules.ToArray(); }
        }
    }
}
