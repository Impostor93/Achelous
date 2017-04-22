namespace Achelous.Facilities.DataAccess
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class DataSourceConnectionInfo
    {
        [System.Xml.Serialization.XmlAttributeAttribute("id")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("server")]
        public string Server { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("instance")]
        public string Instance { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("username")]
        public string Username { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("password")]
        public string Password { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute("additionalConnectionOptions")]
        public string AdditionalConnectionOptions { get; set; }
    }
}
