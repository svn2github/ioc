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
using System.Reflection;
using System.IO;

namespace OpenNETCF.IoC
{
    public class DefaultModuleInfoStore : IModuleInfoStore
    {
        private string m_catalogFilePath;

        public DefaultModuleInfoStore()
        {
            CatalogFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            CatalogFilePath = Path.Combine(m_catalogFilePath, "ProfileCatalog.xml");
        }

        public string GetModuleListXml()
        {
            try
            {
                using (TextReader reader = File.OpenText(m_catalogFilePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }

        public string CatalogFilePath
        {
            get { return m_catalogFilePath; }
            set
            {
                Guard.ArgumentNotNullOrEmptyString(value, "CatalogFilePath");
                m_catalogFilePath = value;
            }
        }
    }
}
