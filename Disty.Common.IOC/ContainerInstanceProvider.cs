using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DiagnosticsContract = System.Diagnostics.Contracts.Contract;

namespace Disty.Common.IOC
{
    public static class ContainerInstanceProvider
    {
        /// <summary>
        /// See GetContainerInstance(Assembly)
        /// </summary>
        /// <returns></returns>
        public static IContainer GetContainerInstance()
        {
            return GetContainerInstance(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Scans current domain included previously unloaded assemblies for IRegister implementations.
        /// Assemblies in common will be called first, followed by any others.
        /// Calling assembly will be last
        /// </summary>
        /// <param name="callingAssembly">
        /// Used to determine which IRegister implementors should be run last.
        /// Useful when you need to override some previous registrations
        /// </param>
        /// <returns>An autofac IContainer</returns>
        public static IContainer GetContainerInstance(Assembly callingAssembly)
        {
            DiagnosticsContract.Ensures(DiagnosticsContract.Result<IContainer>() != null);

            var containerBuilder = new ContainerBuilder();
            var assemblies = AssemblyHelper.AssembliesOrdered("Disty.Common", callingAssembly.FullName);
            var types = TypeHelper.FindTypesThatExtend<IRegister>(assemblies);
            foreach (var type in types)
            {
                Trace.WriteLine("Executing IRegister.Register method within type " + type);
                try
                {
                    ((IRegister)Activator.CreateInstance(type)).Register(containerBuilder);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(string.Format("Failed to register {0}: {1}", type, exception.Message));
                }
            }

            var container = containerBuilder.Build();



            return container;
        }
    }
}
