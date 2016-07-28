#region

using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

#endregion

namespace EppLib.Entities
{
    public abstract class EppBase<T> where T : EppResponse
    {
        private const string UrnIetfParamsXmlNsEpp = "urn:ietf:params:xml:ns:epp-1.0";

        protected static XmlElement CreateDocRoot(XmlDocument doc)
        {
            return CreateDocRoot("epp", doc, "xmlns");
        }

        protected static void PrepareExtensionElement(XmlDocument doc, XmlElement command,
            ICollection<EppExtension> extensions)
        {
            if (extensions != null && extensions.Any())
            {
                var extensionElement = CreateElement(doc, "extension");

                foreach (var extension in extensions)
                {
                    var extensionNode = doc.ImportNode(extension.ToXml(doc), true);

                    extensionElement.AppendChild(extensionNode);
                }

                command.AppendChild(extensionElement);
            }
        }

        protected static XmlElement CreateElement(XmlDocument doc, string qualifiedName)
        {
            return doc.CreateElement(qualifiedName, UrnIetfParamsXmlNsEpp);
        }

        private static XmlElement CreateElement(XmlDocument doc, string qualifiedName, string namespaceUri)
        {
            return doc.CreateElement(qualifiedName, namespaceUri);
        }

        private static XmlElement CreateDocRoot(string rootString, XmlDocument doc, string prefix)
        {
            var schema = new XmlSchema();
            schema.Namespaces.Add(prefix, UrnIetfParamsXmlNsEpp);
            doc.Schemas.Add(schema);

            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", "no"));

            var root = doc.CreateElement(rootString, UrnIetfParamsXmlNsEpp);
            root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            root.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:epp-1.0 epp-1.0.xsd");
            return root;
        }

        protected static XmlElement AddXmlElement(XmlDocument doc, XmlElement containingElement, string tagName,
            string value)
        {
            var xmlElement = CreateElement(doc, tagName);

            if (!string.IsNullOrEmpty(value))
            {
                xmlElement.AppendChild(doc.CreateTextNode(value));
            }
            containingElement.AppendChild(xmlElement);
            return xmlElement;
        }

        protected static XmlElement AddXmlElement(XmlDocument doc, XmlElement containingElement, string tagName,
            string value, string namespaceUri)
        {
            var xmlElement = CreateElement(doc, tagName, namespaceUri);

            if (!string.IsNullOrEmpty(value))
            {
                xmlElement.AppendChild(doc.CreateTextNode(value));
            }
            containingElement.AppendChild(xmlElement);
            return xmlElement;
        }

        public abstract XmlDocument ToXml();
        public abstract T FromBytes(byte[] bytes);
    }
}