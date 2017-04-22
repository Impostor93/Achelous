using System;
using System.Collections.Generic;
using System.Linq;
using Achelous.DomainModeling;

namespace Achelous.Facilities.Command
{
    internal class DefaultCombineResultStrategy : ICombineResultStrategy
    {
        public IResult CombineResults(params IResult[] results)
        {
            var listOfEntities = new List<IEntity>();
            foreach (var result in results)
            {
                listOfEntities.AddRange(result.CompletedCommandResult);
            }

            return new CommandResult(results.Any(e => e.IsCompletedSuccessfully == false) ? false : true, listOfEntities);
        }
    }
}