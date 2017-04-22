using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public interface IResult
    {
        bool IsCompletedSuccessfully { get; }
        IList<IEntity> CompletedCommandResult { get; }
    }
}