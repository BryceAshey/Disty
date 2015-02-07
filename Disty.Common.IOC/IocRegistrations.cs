using Autofac;

namespace Disty.Common.IOC
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterType<CurrentPrincipalProxy>().As<ICurrentPrincipalProxy>().SingleInstance();
        }
    }
}