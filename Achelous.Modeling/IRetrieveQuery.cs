using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public interface IRetrieveQuery
    {
        IList<string> Select { get; }
        string From { get; }
        QueryExpression Where { get; }
    }
}