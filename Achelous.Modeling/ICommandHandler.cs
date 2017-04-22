namespace Achelous.DomainModeling
{
    public interface ICommandHandler<T>
        where T : ICommand
    {
        IResult Handle(T command);
    }
}
