using System;
using Achelous.Commands;
using Achelous.Commands.CommandHandlers;
using Achelous.DomainModeling;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Achelous.Facilities.Command
{
    public class CommandFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Register(Component.For<ICommandHandlerRegister>().ImplementedBy<DefaultCommandHandlerRegister>());
            kernel.Register(Component.For<ICommandRetryStrategy<IResult>>().ImplementedBy<DefaultCommandRetryStrategy>());
            kernel.Register(Component.For<ICombineResultStrategy>().ImplementedBy<DefaultCombineResultStrategy>());
            kernel.Register(Component.For<IErrorHandlingStrategy>().ImplementedBy<ErrorHandlingStrategy>());
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
