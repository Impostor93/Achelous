using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public class QueryExpression
    {
        public List<Predicates> Expressions { get; private set; }
        public LogicalOperator LogicalOperator { get; set; }

        public List<QueryExpression> InnerQueries { get; private set; }

        public QueryExpression()
        {
            Expressions = new List<Predicates>();
            LogicalOperator = LogicalOperator.And;

            InnerQueries = new List<QueryExpression>();
        }

        public QueryExpression(Predicates expression) :
            this()
        {
            Expressions.Add(expression);
        }
    }
}
