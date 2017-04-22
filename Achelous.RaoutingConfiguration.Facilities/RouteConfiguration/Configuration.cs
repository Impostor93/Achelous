using System;
using System.Collections.Generic;

namespace Achelous.RaoutingConfiguration
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Routing", Namespace = "", IsNullable = false)]
    public class Configuration : IRouteConfiguration
    {
        [System.Xml.Serialization.XmlArrayItemAttribute("Resource", IsNullable = false)]
        public List<RouteResource> Resources { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("Datasource", IsNullable = false)]
        public List<DataSourceRoute> Datesources { get; set; }
    }
}
