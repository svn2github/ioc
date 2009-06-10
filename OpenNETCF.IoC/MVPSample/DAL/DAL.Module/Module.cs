using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using DAL.Module.Services;
using Infrastructure.Interface.Services;

namespace DAL.Module
{
    public class Module : ModuleInit
    {
        public override void Load()
        {
        }

        public override void AddServices()
        {
            // add a SQL CE Data Service to the services list, register by common interface
            RootWorkItem.Services.AddNew<SQLCEDataService, IDataService>();
        }
    }
}
