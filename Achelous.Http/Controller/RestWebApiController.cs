using System.Collections.Generic;
using System.Web.Http;
using Achelous.Commands;
using Achelous.DomainModeling;

namespace Achelous.Http
{
    public class RestWebApiController : ApiController
    {
        public ICommandHandlerRegister Register { get; set; }

        public IHttpActionResult Get(double version, string resources, string id)
        {
            var comm = new GetCommand(Request.RequestUri.AbsoluteUri, resources, string.Empty);

            var q = Register.HandleCommand(comm);

            return Ok(q);
        }

        private static QueryExpression ParseStringToClause(Queue<char> queryString)
        {
            if (queryString.Count <= 0)
                return null;

            var queryExpression = new QueryExpression();
            var query = new Queue<char>();

            while (queryString.Count > 0)
            {
                var @char = queryString.Dequeue();
                if (new string(query.ToArray()).Contains("or"))
                    queryExpression.LogicalOperator = LogicalOperator.Or;

                if (@char == '(')
                {
                    var innerQuery = ParseStringToClause(queryString);
                    queryExpression.InnerQueries.Add(innerQuery);
                }
                if (@char == ')' || (@char != ')' && queryString.Count <= 0))
                {
                    if (@char != ')' && queryString.Count <= 0)
                        query.Enqueue(@char);

                    var expressionText = string.Empty;
                    var expressionValue = string.Empty;
                    var expressionAttributeName = string.Empty;
                    ExpressionOperator expressionOperator = new ExpressionOperator();
                    ExpressionOperator exOpp = new ExpressionOperator();

                    while (query.Count > 0)
                    {
                        var queryChar = query.Dequeue();
                        expressionText += queryChar.ToString();

                        if ((queryChar == ' ' || queryChar == '(' || queryChar == ')' || query.Count <= 0)
                            && !(queryChar == ' ' && expressionText.Trim().StartsWith("'") && !expressionText.Trim().EndsWith("'")))
                        {
                            expressionText = expressionText.Trim();
                            if (expressionText.ToLower() == "and" || expressionText.ToLower() == "or")
                            {
                                queryExpression.LogicalOperator = expressionText.ToLower() == "and" ? LogicalOperator.And : LogicalOperator.Or;
                                expressionText = string.Empty;
                            }
                            if (expressionText.StartsWith("'") && expressionText.EndsWith("'"))
                            {
                                expressionValue = expressionText;
                            }
                            else if (IsExpressionOperator(expressionText, out exOpp))
                            {
                                expressionOperator = exOpp;
                            }
                            else
                            {
                                expressionAttributeName = expressionText;
                            }
                            expressionText = string.Empty;
                        }

                        if (!string.IsNullOrEmpty(expressionAttributeName) && !string.IsNullOrEmpty(expressionValue))
                        {
                            queryExpression.Expressions.Add(new Predicates(expressionAttributeName, expressionOperator, expressionValue));
                            expressionAttributeName = string.Empty;
                            expressionValue = string.Empty;
                        }
                    }

                    return queryExpression;
                }
                else
                {
                    query.Enqueue(@char);
                    if (new string(query.ToArray()).Equals("( Or"))
                        queryExpression.LogicalOperator = LogicalOperator.Or;
                }
            }

            return queryExpression;
        }

        private static bool IsExpressionOperator(string expressionText, out ExpressionOperator exOpp)
        {
            switch (expressionText.Trim())
            {
                case "=": exOpp = ExpressionOperator.Equal; return true;
                case "<": exOpp = ExpressionOperator.LessThan; return true;
                case ">": exOpp = ExpressionOperator.GreaterThan; return true;
                case "<=": exOpp = ExpressionOperator.LessOrEqual; return true;
                case ">=": exOpp = ExpressionOperator.GreaterOrEqual; return true;
                case "!=": exOpp = ExpressionOperator.NotEqual; return true;

                default: exOpp = default(ExpressionOperator); return false;
            }
        }
    }
}
