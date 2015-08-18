using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Disty.Common.Data.Repositories;
using Disty.Common.IOC;

namespace Disty.Service.Test.Unit
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c => new FakeItEasy.Fake<IDistributionListRepository>().FakedObject).As<IDistributionListRepository>();
            containerBuilder.Register(c => new FakeItEasy.Fake<IEmailRepository>().FakedObject).As<IEmailRepository>();
        }
    }
}
