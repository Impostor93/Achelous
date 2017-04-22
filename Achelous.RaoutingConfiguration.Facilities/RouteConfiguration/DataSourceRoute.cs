using System;
using System.Collections.Generic;

namespace Achelous.RaoutingConfiguration
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class DataSourceRoute
    {
        public DataSourceRouteRules Rules { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("type")]
        public string Type { get; set; }
    }
}
