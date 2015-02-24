using Autofac;
using log4net;

namespace Disty.Common.IOC
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterType<CurrentPrincipalProxy>().As<ICurrentPrincipalProxy>().SingleInstance();

            containerBuilder.Register(l => LogManager.GetLogger(Common.Contract.Constants.Global.DefaultLogName)).As<ILog>().SingleInstance();
            containerBuilder.RegisterAssemblyTypes(typeof (Disty.Common.Contract.RequestContext).Assembly);
        }
    }
}