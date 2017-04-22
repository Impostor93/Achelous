using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Achelous.DomainModeling;

namespace Achelous.Facilities.DataAccess
{
    public class DataSourceRegister : IDataSourceRegister
    {
        public DataSourceRegister(IDataAccessConfiguration configuration)
        {
            map = new Dictionary<string, Type>();
            this.configuration = configuration;
        }

        public IRepository FindDataSource(string datasourceId)
        {
            if (!map.ContainsKey(datasourceId))
                throw new NotSupportedException($"There is no registrated data source with id '{datasourceId}'");

            var conf = configuration.ConnectionInfo.SingleOrDefault(e => e.Id == datasourceId);
            if (ReferenceEquals(conf, null))
                throw new NotSupportedException($"There is no configuration for data source '{datasourceId}'");

            var dataSource = map[datasourceId];

            var repo = (IDataSouerce)Activator.CreateInstance(dataSource, new Object[] { });
            repo.CreateConnectionString(conf.Server, conf.Instance, conf.Username, conf.Password, conf.AdditionalConnectionOptions);

            return (IRepository)repo;
        }

        public void Register(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    Register(type);
                }
            }
        }

        public void Register(Type dataAccessType)
        {
            var attribute = dataAccessType.GetCustomAttribute(typeof(DataSourceAttribute));
            if (ReferenceEquals(attribute, null) == false && typeof(IRepository).IsAssignableFrom(dataAccessType) && typeof(IDataSouerce).IsAssignableFrom(dataAccessType))
            {
                map.Add(((DataSourceAttribute)attribute).ConnectionInfoId, dataAccessType);
            }
        }

        private Dictionary<string, Type> map;
        private IDataAccessConfiguration configuration;
    }
}