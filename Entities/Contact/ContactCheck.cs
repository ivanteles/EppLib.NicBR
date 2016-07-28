#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactCheck : ContactBase<ContactCheckResponse>
    {
        private readonly IList<string> _contactIds;

        public ContactCheck(string contactId)
        {
            _contactIds = new List<string> {contactId};
        }

        public ContactCheck(IList<string> contactIds)
        {
            _contactIds = contactIds;
        }

        public override ContactCheckResponse FromBytes(byte[] bytes)
        {
            return new ContactCheckResponse(bytes);
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var contactCheck = BuildCommandElement(doc, "check", commandRootElement);
            contactCheck.SetAttribute("xmlns:contac", "urn:ietf:params:xml:ns:contact-1.0");
            contactCheck.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:contact-1.0 contact-1.0.xsd");

            foreach (var contactId in _contactIds)
            {
                AddXmlElement(doc, contactCheck, "contact:id", contactId, NamespaceUri);
            }

            return contactCheck;
        }
    }
}