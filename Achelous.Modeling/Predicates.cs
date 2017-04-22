using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public class Predicates
    {
        public string AttributeName { get; private set; }

        public object Value { get; private set; }

        public ExpressionOperator Operator { get; private set; }

        public List<Predicates> InnerExpressions { get; }

        public Predicates(string attributeName, ExpressionOperator @operator, object value)
        {
            AttributeName = attributeName;
            Operator = @operator;
            Value = value;

            InnerExpressions = new List<Predicates>();
        }
    }
}
