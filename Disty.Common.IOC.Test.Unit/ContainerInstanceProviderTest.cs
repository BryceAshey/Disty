using System;
using System.Reflection;
using Autofac;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disty.Common.IOC.Test.Unit
{
    [TestClass]
    public class ContainerInstanceProviderTest
    {
        [TestMethod]
        public void CanGetAContainerWithoutAssembly()
        {
            var container = ContainerInstanceProvider.GetContainerInstance();
            Assert.IsNotNull(container);
        }

        [TestMethod]
        public void CanGetAContainerWithAssembly()
        {
            var container = ContainerInstanceProvider.GetContainerInstance(Assembly.GetCallingAssembly());
            Assert.IsNotNull(container);
            Assert.IsTrue(container is IContainer);
        }

        [TestMethod]
        public void CanActuallyResolveFromContainer()
        {
            var container = ContainerInstanceProvider.GetContainerInstance(Assembly.GetCallingAssembly());
            var testObj = container.Resolve<ILog>();
            Assert.IsNotNull(testObj);
            Assert.IsTrue(testObj is ILog);
        }
    }
}
