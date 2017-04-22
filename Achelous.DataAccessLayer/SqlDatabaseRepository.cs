using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Achelous.Facilities.DataAccess;
using Achelous.DomainModeling;
using System.Linq;

namespace Achelous.SqlDataAccess
{
    [DataSource("DataBase petar")]
    public class DatabaseRepository : IRepository, IDataSouerce
    {
        string connectionString;

        public void CreateConnectionString(string server, string instance, string username, string password, string additionalDetails)
        {
            var connectionStringBuilder = new DbConnectionStringBuilder();
            connectionStringBuilder.Add("Server", server);
            connectionStringBuilder.Add("Initial Catalog", instance);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                connectionStringBuilder.Add("Integrated Security", true);
            }
            else
            {
                connectionStringBuilder.Add("User ID", username);
                connectionStringBuilder.Add("Password", password);
                connectionStringBuilder.Add("Integrated Security", false);
            }

            connectionString = (connectionStringBuilder.ConnectionString + additionalDetails);
        }

        public IResult Create(IEntity entity)
        {
            throw new NotImplementedException();
        }
        public IResult Retrieve(IRetrieveQuery query)
        {
            var connection = new SqlConnection(connectionString);

            var command = new SqlCommand();
            command.Connection = connection;

            var select = query.Select.Count == 0 ? "*" : string.Join(",", query.Select);
            var commandText = $"SELECT {select} FROM {query.From}";

            var extractedQuery = ExtractQuery(query.Where);
            commandText += extractedQuery.Length > 0 ? $" WHERE {extractedQuery}" : string.Empty;

            command.CommandText = commandText;

            var results = new List<IEntity>();
            try
            {
                connection.Open();

                using (connection)
                {
                    var reader = command.ExecuteReader();
                    var tableSchema = reader.GetSchemaTable();

                    var columns = tableSchema.Select().Select(e => e.ItemArray.First());

                    while (reader.Read())
                    {
                        var attributes = new Dictionary<string, object>();
                        foreach (var column in columns)
                        {
                            var columnName = column.ToString();
                            attributes.Add(columnName, reader[columnName]);
                        }

                        results.Add(new Entity(Guid.Empty, query.From, attributes));
                    }
                }

                return new CommandResult(true, results);
            }
            catch (Exception ex)
            {
                results.Add(new Entity(Guid.Empty, "Error", new Dictionary<string, object>() { { "Error", ex.Message } }));
                return new CommandResult(true, results);
            }
        }

        public IResult Update(IRetrieveQuery query, IEntity entity)
        {
            throw new NotImplementedException();
        }
        public IResult Delete(IRetrieveQuery query)
        {
            throw new NotImplementedException();
        }

        private static string ExtractQuery(QueryExpression where, Nullable<LogicalOperator> parentOperator = null)
        {
            var whereClause = string.Empty;
            if (where.Expressions.Count <= 0 && where.InnerQueries.Count <= 0)
                return string.Empty;

            List<string> expressions = new List<string>();
            foreach (var expression in where.Expressions)
            {
                expressions.Add(ExpressionToString(expression));
            }

            if (expressions.Count > 0)
            {
                if (parentOperator.HasValue)
                    whereClause += $" {parentOperator.Value} ";

                whereClause += $"({string.Join($" {where.LogicalOperator.ToString()} ", expressions)}";
            }
            else
            {
                whereClause += "(";
            }

            foreach (var inner in where.InnerQueries)
            {
                whereClause += ExtractQuery(inner, inner.LogicalOperator);
            }

            whereClause += ")";

            whereClause = whereClause.Replace("( Or ", " Or (").Replace("( And ", " And (");

            return parentOperator.HasValue ? whereClause : whereClause.TrimStart(" And ".ToArray()).TrimStart(" Or ".ToArray());
        }

        private static string ExpressionToString(Predicates expression)
        {
            return $"({expression.AttributeName} {ConvertToSqlOperator(expression.Operator)} '{expression.Value}')";
        }

        private static string ConvertToSqlOperator(ExpressionOperator @operator)
        {
            switch (@operator)
            {
                case ExpressionOperator.Equal: return "=";
                case ExpressionOperator.NotEqual: return "<>";
                case ExpressionOperator.LessThan: return "<";
                case ExpressionOperator.GreaterThan: return ">";
                case ExpressionOperator.LessOrEqual: return "<=";
                case ExpressionOperator.GreaterOrEqual: return ">=";

                default: throw new NotSupportedException();
            }
        }
    }
}
