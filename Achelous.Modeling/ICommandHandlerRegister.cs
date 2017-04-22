using System.Reflection;
using System.Threading.Tasks;

namespace Achelous.DomainModeling
{
    public interface ICommandHandlerRegister
    {
        void RegisterHandler(params Assembly[] handlerAssemblies);

        IResult HandleCommand(ICommand command);

        Task<IResult> HandleCommandAsync(ICommand command);

        IResult HandleMultipleCommands(params ICommand[] commands);

        Task<IResult> HandleMultipleCommandsAsync(params ICommand[] commands);

        IResult HandleMultipleCommandsParallel(params ICommand[] command);
    }
}
