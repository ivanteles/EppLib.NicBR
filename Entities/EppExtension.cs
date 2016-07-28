#region

using System.Xml;

#endregion

namespace EppLib.Entities
{
    public abstract class EppExtension
    {
        protected abstract string Namespace { get; set; }

        public abstract XmlNode ToXml(XmlDocument doc);

        protected XmlElement CreateElement(XmlDocument doc, string qualifiedName)
        {
            return doc.CreateElement(qualifiedName, Namespace);
        }

        private XmlElement CreateElement(XmlDocument doc, string qualifiedName, string namespaceURI)
        {
            return doc.CreateElement(qualifiedName, namespaceURI);
        }

        protected XmlElement AddXmlElement(XmlDocument doc, XmlElement containingElement, string tagName, string value)
        {
            var xml_element = CreateElement(doc, tagName);

            if (!string.IsNullOrEmpty(value))
            {
                xml_element.AppendChild(doc.CreateTextNode(value));
            }
            containingElement.AppendChild(xml_element);
            return xml_element;
        }

        protected XmlElement AddXmlElement(XmlDocument doc, XmlElement containingElement, string tagName, string value,
            string namespaceUri)
        {
            var xml_element = CreateElement(doc, tagName, namespaceUri);

            if (!string.IsNullOrEmpty(value))
            {
                xml_element.AppendChild(doc.CreateTextNode(value));
            }
            containingElement.AppendChild(xml_element);
            return xml_element;
        }
    }
}