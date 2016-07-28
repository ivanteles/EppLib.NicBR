#region

using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactDelete : ContactBase<ContactDeleteResponse>
    {
        private readonly string _mId;

        public ContactDelete(string mId)
        {
            _mId = mId;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var contactDelete = BuildCommandElement(doc, "delete", commandRootElement);
            contactDelete.SetAttribute("xmlns:contac", "urn:ietf:params:xml:ns:contact-1.0");
            contactDelete.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:contact-1.0 contact-1.0.xsd");

            AddXmlElement(doc, contactDelete, "contact:id", _mId, NamespaceUri);

            return contactDelete;
        }

        public override ContactDeleteResponse FromBytes(byte[] bytes)
        {
            return new ContactDeleteResponse(bytes);
        }
    }
}