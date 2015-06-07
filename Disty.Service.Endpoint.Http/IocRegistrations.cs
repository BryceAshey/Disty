using Autofac;
using Disty.Common.IOC;

namespace Disty.Service.Endpoint.Http
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DistributionListController>().As<IDistributionListController>();
            containerBuilder.RegisterType<EmailController>().As<IEmailController>();
            containerBuilder.RegisterAssemblyTypes(typeof(Disty.Service.Endpoint.Http.IocRegistrations).Assembly);
        }
    }
}