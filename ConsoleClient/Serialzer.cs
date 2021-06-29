using System.IO;
using System.Xml.Serialization;

namespace ConsoleClient
{
    public class Serialzer
    {

        public string Serialize<T>(T ObjectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }

        public T Deserializer<T>(string input) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using StringReader stringReader = new StringReader(input);
            return (T)serializer.Deserialize(stringReader);
        }
    }
}
