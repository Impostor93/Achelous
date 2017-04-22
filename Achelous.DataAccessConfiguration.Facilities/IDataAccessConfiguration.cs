using System.Collections.Generic;

namespace Achelous.Facilities.DataAccess
{
    public interface IDataAccessConfiguration
    {
        List<DataSourceConnectionInfo> ConnectionInfo { get; }
    }
}
