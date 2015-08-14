using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disty.Common.IOC.Test.Unit
{
    [TestClass]
    public class AssemblyHelperTest
    {
        [TestMethod]
        public void CanFindAssemblies()
        {
            var assemblyList = new List<Assembly>(AssemblyHelper.Assemblies);
            Assert.AreEqual(assemblyList.Count, 6);
        }

        [TestMethod]
        public void CanGetAssembliesInOrder()
        {
            var assemblyList = new List<Assembly>(AssemblyHelper.AssembliesOrdered("Disty.Common", null));
            Assert.IsTrue(assemblyList[0].FullName.Contains("Disty.Common"));
        }

    }
}
