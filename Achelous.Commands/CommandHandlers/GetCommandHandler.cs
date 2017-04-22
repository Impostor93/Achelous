using System.Collections.Generic;
using Achelous.DomainModeling;
using Achelous.RoutingEngine;

namespace Achelous.Commands.CommandHandlers
{
    public class GetCommandHandler : ICommandHandler<GetCommand>
    {
        IRoutingEngine engine;
        ICombineResultStrategy combineStrategy;

        public GetCommandHandler(IRoutingEngine engine, ICombineResultStrategy combineStrategy)
        {
            this.engine = engine;
            this.combineStrategy = combineStrategy;
        }

        public IResult Handle(GetCommand command)
        {
            var repositories = engine.GetDataSources(command.RequestUrl, command.Resource, command.SecondaryResource);
            var results = new IResult[repositories.Count];

            var query = new RetrieveQuery(command.Resource, new List<string>(), command.Query);

            for (var i = 0; i < repositories.Count; i++)
            {
                results[i] = repositories[i].Retrieve(query);
            }

            return combineStrategy.CombineResults(results);
        }
    }
}
