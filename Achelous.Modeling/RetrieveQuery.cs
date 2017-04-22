using System;
using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public class RetrieveQuery : IRetrieveQuery
    {
        public string From { get; private set; }

        public IList<string> Select { get; private set; }

        public QueryExpression Where { get; private set; }

        public RetrieveQuery(string from, IList<string> select, QueryExpression expression)
        {
            From = from;
            Select = select;
            Where = expression;
        }
    }
}
