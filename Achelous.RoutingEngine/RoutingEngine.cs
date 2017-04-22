using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Achelous.DomainModeling;
using Achelous.RaoutingConfiguration;

namespace Achelous.RoutingEngine
{
    public class RoutingEngine : IRoutingEngine
    {
        IRouteConfiguration configuration;
        private IDataSourceRegister dataSourceRegister;

        public RoutingEngine(IRouteConfiguration configuration, IDataSourceRegister dataSourceRegister)
        {
            this.configuration = configuration;
            this.dataSourceRegister = dataSourceRegister;
        }

        public IList<IRepository> GetDataSources(string url, string resource, string secondaryResource)
        {
            if (!string.IsNullOrEmpty(secondaryResource) && !this.configuration.Resources.Any(e => e.Id == secondaryResource))
                throw new NotSupportedException($"The resource '{secondaryResource}' is not supported!");

            var repositories = new List<IRepository>();

            foreach (var datasource in this.configuration.Datesources)
            {
                if (datasource.Rules.And.Count() > 0)
                {
                    var andRules = datasource.Rules.And;

                    var andRegexRule = andRules.Where(e => e.Type == "RegEx");
                    var andResourceRule = andRules.Where(e => e.Type == "Resurce");

                    var regExRules = true;
                    var resourceRules = true;

                    if (andRegexRule.Count() > 0)
                        regExRules = andRegexRule.All(e => new Regex(e.Value).IsMatch(url));

                    if (andResourceRule.Count() > 0)
                        resourceRules = andResourceRule.All(e => e.Value == resource || e.Value == secondaryResource);

                    if (regExRules && resourceRules)
                        repositories.Add(this.dataSourceRegister.FindDataSource(datasource.Id));
                }

                if (datasource.Rules.Or.Count() > 0)
                {
                    var andRules = datasource.Rules.Or;

                    var orRegexRule = andRules.Where(e => e.Type == "RegEx");
                    var orResourceRule = andRules.Where(e => e.Type == "Resurce");

                    var regExRules = true;
                    var resourceRules = true;

                    if (orRegexRule.Count() > 0)
                        regExRules = orRegexRule.Any(e => new Regex(e.Value).IsMatch(url));

                    if (orResourceRule.Count() > 0)
                        resourceRules = orResourceRule.Any(e => e.Value == resource || e.Value == secondaryResource);

                    if (regExRules || resourceRules)
                        repositories.Add(this.dataSourceRegister.FindDataSource(datasource.Id));
                }
            }

            return repositories;
        }
    }
}
