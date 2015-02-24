using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Disty.Common.IOC;

namespace Disty.Service.Endpoint.Http
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistributionListController>().As<IDistributionListController>();
            containerBuilder.RegisterAssemblyTypes(typeof(Disty.Service.Endpoint.Http.IocRegistrations).Assembly);
        }
    }
}