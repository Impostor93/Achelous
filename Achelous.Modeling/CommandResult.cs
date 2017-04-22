using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public class CommandResult : IResult
    {
        public bool IsCompletedSuccessfully { get; private set; }
        public IList<IEntity> CompletedCommandResult { get; private set; }

        public CommandResult(bool isCompletedSuccessfully, IList<IEntity> completedCommandResult)
        {
            IsCompletedSuccessfully = isCompletedSuccessfully;
            CompletedCommandResult = completedCommandResult;    
        }
    }
}
