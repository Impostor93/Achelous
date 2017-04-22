using System;
using System.Reflection;

namespace Achelous.DomainModeling
{
    public interface IDataSourceRegister
    {
        void Register(params Assembly[] assemblies);
        void Register(Type dataAccessType);
        IRepository FindDataSource(string datasourceId);
    }
}
