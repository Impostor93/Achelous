using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Achelous.DomainModeling
{
    public class ConfigurationBuilder<T>
    {
        private string filePath;

        public ConfigurationBuilder(string filePath)
        {
            this.filePath = filePath;
        }

        public T Build()
        {
            var configurationXmlAsText = File.ReadAllText(filePath);

            var serializer = new XmlSerializer(typeof(T));
            T result;

            using (TextReader reader = new StringReader(configurationXmlAsText))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
