#region

using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactUpdate : ContactBase<ContactUpdateResponse>
    {
        public ContactChange ContactChange;
        public string ContactId;

        public EppContactUpdateAddRemove ToAdd;
        public EppContactUpdateAddRemove ToRemove;


        public ContactUpdate(string contactId)
        {
            ContactId = contactId;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            if (ContactId == null)
            {
                throw new EppException("missing contact id");
            }

            var contactUpdate = BuildCommandElement(doc, "update", commandRootElement);
            contactUpdate.SetAttribute("xmlns:contac", "urn:ietf:params:xml:ns:contact-1.0");
            contactUpdate.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:contact-1.0 contact-1.0.xsd");


            AddXmlElement(doc, contactUpdate, "contact:id", ContactId, NamespaceUri);

            var addElement = GetAddRemoveElement(doc, ToAdd);

            if (addElement != null)
            {
                contactUpdate.AppendChild(addElement);
            }

            var removeElement = GetAddRemoveElement(doc, ToRemove);

            if (removeElement != null)
            {
                contactUpdate.AppendChild(removeElement);
            }

            if (ContactChange != null)
            {
                var changeElement = doc.CreateElement("contact:chg", NamespaceUri);

                var xml = AddressToXml(doc, "contact:postalInfo", ContactChange.PostalInfo);

                changeElement.AppendChild(xml);

                if (ContactChange.Voice != null)
                {
                    var voice = AddXmlElement(doc, changeElement, "contact:voice", ContactChange.Voice.Value,
                        NamespaceUri);

                    if (!string.IsNullOrEmpty(ContactChange.Voice.Extension))
                    {
                        voice.SetAttribute("x", ContactChange.Voice.Extension);
                    }
                }

                if (ContactChange.Fax != null)
                {
                    var voice = AddXmlElement(doc, changeElement, "contact:fax", ContactChange.Fax.Value, NamespaceUri);

                    if (!string.IsNullOrEmpty(ContactChange.Fax.Extension))
                    {
                        voice.SetAttribute("x", ContactChange.Fax.Extension);
                    }
                }

                if (ContactChange.Email != null)
                {
                    AddXmlElement(doc, changeElement, "contact:email", ContactChange.Email, NamespaceUri);
                }

                contactUpdate.AppendChild(changeElement);
            }

            return contactUpdate;
        }

        private static XmlElement GetAddRemoveElement(XmlDocument doc, EppContactUpdateAddRemove addRemoveItems)
        {
            XmlElement addRemoveElement = null;

            if (addRemoveItems != null)
            {
                if (addRemoveItems.Status != null &&
                    addRemoveItems.Status.Count > 0)
                {
                    foreach (var status in addRemoveItems.Status)
                    {
                        var statusElement = AddXmlElement(doc, addRemoveElement, "contact:status", status.Value);

                        statusElement.SetAttribute("s", status.Type);

                        if (status.Lang != null)
                        {
                            statusElement.SetAttribute("lang", status.Lang);
                        }
                    }
                }
            }


            return addRemoveElement;
        }

        public override ContactUpdateResponse FromBytes(byte[] bytes)
        {
            return new ContactUpdateResponse(bytes);
        }
    }
}