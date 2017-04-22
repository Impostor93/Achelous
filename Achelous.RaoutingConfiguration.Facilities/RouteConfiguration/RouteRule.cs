using System;

namespace Achelous.RaoutingConfiguration
{

    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class RouteRule
    {
        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("type")]
        public string Type { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("value")]
        public string Value { get; set; }
    }
}
