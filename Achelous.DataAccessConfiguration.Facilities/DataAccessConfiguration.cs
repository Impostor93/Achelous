using System.Collections.Generic;

namespace Achelous.Facilities.DataAccess
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "DataSourceConnections", Namespace = "", IsNullable = false)]
    public class DataAccessConfiguration : IDataAccessConfiguration
    {
        [System.Xml.Serialization.XmlElementAttribute("ConnectionInfo")]
        public List<DataSourceConnectionInfo> ConnectionInfo { get; set; }
    }
}
