using Autofac;
using Disty.Common.Data.Repositories;
using Disty.Common.IOC;
using Disty.Model.MsSql.Repositories;
using Disty.Model.MsSql.Repositories;

namespace Disty.Model.MsSql
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistributionDeptRepository>().As<IDistributionDeptRepository>();
            containerBuilder.RegisterType<DistributionListRepository>().As<IDistributionListRepository>();
            containerBuilder.RegisterType<EmailRepository>().As<IEmailRepository>();
        }
    }
}