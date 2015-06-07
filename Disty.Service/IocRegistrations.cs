using Autofac;
using Disty.Common.IOC;
using Disty.Service.Interfaces;

namespace Disty.Service
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistributionListService>().As<IDistributionListService>();
            containerBuilder.RegisterType<EmailService>().As<IEmailService>();
        }
    }
}
