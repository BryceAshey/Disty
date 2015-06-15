using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Disty.Common.IOC;
using Disty.Service.Interfaces;

namespace Disty.Service.Client
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistyClient>().As<IDistyClient>();
            containerBuilder.RegisterType<ListClient>().As<IDistributionListService>();
            containerBuilder.RegisterAssemblyTypes(typeof(Disty.Common.Contract.DistyEntity).Assembly);
        }
    }
}
