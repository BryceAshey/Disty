using System;
using Autofac;
using Disty.Common.IOC;
using Disty.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disty.Service.Test.Unit
{
    [TestClass]
    public class DistributionListServiceTest
    {
        private IContainer _container;

        public  DistributionListServiceTest()
        {
            _container = ContainerInstanceProvider.GetContainerInstance();
        }

        [TestMethod]
        public void CanResolve()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IDistributionListService>();
                Assert.IsInstanceOfType(service, typeof(DistributionListService));
            }
        }
    }
}
