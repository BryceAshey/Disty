using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Disty.Common.IOC;
using MyCouch;

namespace Disty.Common.Data.CouchDB
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c => new MyCouchStore(ConfigurationManager.ConnectionStrings[Disty.Common.Contract.Constants.Global.Connections.PrimaryDB].ConnectionString))
                .As(typeof(IMyCouchStore))
                .InstancePerLifetimeScope();

            containerBuilder.RegisterGeneric(typeof(CouchClient<>))
                .As(typeof(IDataClient<>))
                .InstancePerLifetimeScope();
        }
    }
}
