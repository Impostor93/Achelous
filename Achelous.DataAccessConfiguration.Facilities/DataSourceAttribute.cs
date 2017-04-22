using System;

namespace Achelous.Facilities.DataAccess
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataSourceAttribute : Attribute
    {
        public string ConnectionInfoId { get; private set; }

        public DataSourceAttribute(string connectionInfoId)
        {
            ConnectionInfoId = connectionInfoId;
        }
    }
}
