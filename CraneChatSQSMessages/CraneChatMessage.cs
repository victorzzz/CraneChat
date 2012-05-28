using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Reflection;

namespace CraneChat.SQSMessages
{
    [Serializable]
    public abstract class CraneChatMessage
    {
        protected CraneChatMessage()
        {
        }

        public string ToXML()
        {
            string result = null;

            XmlSerializer serializer = new XmlSerializer(this.GetType());
            using (StringWriter stringWritter = new StringWriter())
            {
                serializer.Serialize(stringWritter, this);
                result = stringWritter.ToString();
            }
            return result;
        }

        public static CraneChatMessage FromXML(string xmlString, string typeName = null)
        {
            if (null == typeName)
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
                {
                    // Parse and try to find first Element to determine class name.
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            typeName = reader.Name;
                            break;
                        }
                    }
                }
            }

            CraneChatMessage message = null;
            try
            {
                Type typeToDesirialize = Assembly.GetExecutingAssembly().GetType("CraneChat.SQSMessages." + typeName);
                using (StringReader stringReader = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeToDesirialize);
                    message = serializer.Deserialize(stringReader) as CraneChatMessage;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return message;
        }
    }
}
