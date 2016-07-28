#region

using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactInfo : ContactBase<ContactInfoResponse>
    {
        private readonly string _id;

        public ContactInfo(string id)
        {
            _id = id;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var contactInfo = BuildCommandElement(doc, "info", commandRootElement);
            contactInfo.SetAttribute("xmlns:contac", "urn:ietf:params:xml:ns:contact-1.0");
            contactInfo.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:contact-1.0 contact-1.0.xsd");

            AddXmlElement(doc, contactInfo, "contact:id", _id, NamespaceUri);

            return contactInfo;
        }

        public override ContactInfoResponse FromBytes(byte[] bytes)
        {
            return new ContactInfoResponse(bytes);
        }
    }
}