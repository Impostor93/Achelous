using System.Collections.Generic;
using Achelous.DomainModeling;

namespace Achelous.RoutingEngine
{
    public interface IRoutingEngine
    {
        IList<IRepository> GetDataSources(string url, string resource, string secondaryResource);
    }
}
