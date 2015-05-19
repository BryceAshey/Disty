using Autofac;
using Disty.Common.IOC;
using Disty.Model.MySql.Repositories;

namespace Disty.Model.MySql
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistributionDeptRepository>().As<IDistributionDeptRepository>();
            containerBuilder.RegisterType<DistributionListRepository>().As<IDistributionListRepository>();
        }
    }
}