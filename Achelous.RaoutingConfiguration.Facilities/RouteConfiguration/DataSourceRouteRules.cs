using System.Collections.Generic;

namespace Achelous.RaoutingConfiguration
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class DataSourceRouteRules
    {
        [System.Xml.Serialization.XmlArrayItemAttribute("Rule", IsNullable = false)]
        public List<RouteRule> Or { get; set; }

        [System.Xml.Serialization.XmlArrayItemAttribute("Rule", IsNullable = false)]
        public List<RouteRule> And { get; set; }
    }
}
