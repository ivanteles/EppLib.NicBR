#region

using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactCreate : ContactBase<ContactCreateResponse>
    {
        protected readonly Contact Contact;

        public ContactCreate(Contact contact)
        {
            Contact = contact;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            if (Contact.Id == null)
            {
                throw new EppException("missing contact id");
            }

            var contactCreate = BuildCommandElement(doc, "create", commandRootElement);
            contactCreate.SetAttribute("xmlns:contac", "urn:ietf:params:xml:ns:contact-1.0");
            contactCreate.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:contact-1.0 contact-1.0.xsd");

            AddXmlElement(doc, contactCreate, "contact:id", Contact.Id, NamespaceUri);

            var xml = AddressToXml(doc, "contact:postalInfo", Contact.PostalInfo);

            contactCreate.AppendChild(xml);

            if (Contact.Voice != null)
            {
                var voice = AddXmlElement(doc, contactCreate, "contact:voice", Contact.Voice.Value, NamespaceUri);

                if (Contact.Voice.Extension != null)
                {
                    voice.SetAttribute("x", Contact.Voice.Extension);
                }
            }

            if (Contact.Fax != null)
            {
                var voice = AddXmlElement(doc, contactCreate, "contact:fax", Contact.Fax.Value, NamespaceUri);
                if (Contact.Fax.Extension != null)
                {
                    voice.SetAttribute("x", Contact.Fax.Extension);
                }
            }

            if (Contact.Email != null)
            {
                AddXmlElement(doc, contactCreate, "contact:email", Contact.Email, NamespaceUri);
            }

            if (Contact.DiscloseFlag != null)
            {
                var disclose = DiscloseToXml(doc, Contact.DiscloseMask, (bool) Contact.DiscloseFlag);
                contactCreate.AppendChild(disclose);
            }

            return contactCreate;
        }

        public override ContactCreateResponse FromBytes(byte[] bytes)
        {
            return new ContactCreateResponse(bytes);
        }
    }
}