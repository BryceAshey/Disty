using System.Diagnostics.Contracts;
using Autofac;
using DiagnosticsContract = System.Diagnostics.Contracts.Contract;

namespace Disty.Common.IOC
{
    [ContractClass(typeof (RegisterContract))]
    public interface IRegister
    {
        void Register(ContainerBuilder containerBuilder);
    }

    [ContractClassFor(typeof (IRegister))]
    internal abstract class RegisterContract : IRegister
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            DiagnosticsContract.Requires(containerBuilder != null);
        }
    }
}