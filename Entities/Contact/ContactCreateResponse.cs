#region

using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactCreateResponse : EppResponse
    {
        public ContactCreateResponse(byte[] bytes) : base(bytes)
        {
        }

        public string ContactId { get; set; }
        public string DateCreated { get; set; }

        protected override void ProcessDataNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("contact", "urn:ietf:params:xml:ns:contact-1.0");

            var children = doc.SelectSingleNode("//contact:creData", namespaces);

            if (children != null)
            {
                XmlNode node;

                // ContactId
                node = children.SelectSingleNode("contact:id", namespaces);
                if (node != null)
                {
                    ContactId = node.InnerText;
                }

                // DateCreated
                node = children.SelectSingleNode("contact:crDate", namespaces);
                if (node != null)
                {
                    DateCreated = node.InnerText;
                }
            }
        }
    }
}