using Achelous.DomainModeling;

namespace Achelous.Commands
{
    public class GetCommand : ICommand
    {
        public string RequestUrl { get; private set; }
        public string Resource { get; private set; }
        public string SecondaryResource { get; private set; }
        public QueryExpression Query { get; private set; }

        public GetCommand(string requestUrl, string resource, string secondaryResource)
        {
            RequestUrl = requestUrl;
            Resource = resource;
            SecondaryResource = secondaryResource;
            Query = new QueryExpression();
        }
        public GetCommand(string requestUrl, string resource, string secondaryResource, QueryExpression query)
            : this(requestUrl, resource, secondaryResource)
        {
            Query = query;
        }
    }
}
