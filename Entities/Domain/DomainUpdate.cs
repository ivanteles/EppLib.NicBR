#region

using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainUpdate : DomainBase<DomainUpdateResponse>
    {
        private readonly string _domainName;

        public DomainUpdate(string domainName)
        {
            _domainName = domainName;
        }

        public DomainChange DomainChange { get; set; }

        public EppDomainUpdateAddRemove ToAdd { get; } = new EppDomainUpdateAddRemove();

        public EppDomainUpdateAddRemove ToRemove { get; } = new EppDomainUpdateAddRemove();

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainUpdate = BuildCommandElement(doc, "update", commandRootElement);
            domainUpdate.SetAttribute("xmlns:domain", "urn:ietf:params:xml:ns:domain-1.0");
            domainUpdate.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:domain-1.0 domain-1.0.xsd");

            AddXmlElement(doc, domainUpdate, "domain:name", _domainName, NamespaceUri);

            var addElement = GetAddRemoveElement(doc, ToAdd, "domain:add", NamespaceUri);

            if (addElement != null)
            {
                domainUpdate.AppendChild(addElement);
            }

            var removeElement = GetAddRemoveElement(doc, ToRemove, "domain:rem", NamespaceUri);

            if (removeElement != null)
            {
                domainUpdate.AppendChild(removeElement);
            }

            if (DomainChange != null)
            {
                var changeElement = doc.CreateElement("domain:chg", NamespaceUri);

                if (DomainChange.RegistrantContactId != null)
                {
                    AddXmlElement(doc, changeElement, "domain:registrant", DomainChange.RegistrantContactId,
                        NamespaceUri);
                }

                domainUpdate.AppendChild(changeElement);
            }

            return domainUpdate;
        }

        private XmlElement GetAddRemoveElement(XmlDocument doc, EppDomainUpdateAddRemove addRemoveItems,
            string tagName, string namespaceUri)
        {
            XmlElement add_remove_element = null;

            if (addRemoveItems != null)
            {
                if (addRemoveItems.NameServers != null &&
                    addRemoveItems.NameServers.Count > 0)
                {
                    add_remove_element = doc.CreateElement(tagName, namespaceUri);

                    add_remove_element.AppendChild(CreateNameServerElement(doc, addRemoveItems.NameServers));
                }

                if (addRemoveItems.DomainContacts != null &&
                    addRemoveItems.DomainContacts.Count > 0)
                {
                    if (add_remove_element == null)
                    {
                        add_remove_element = doc.CreateElement(tagName, namespaceUri);
                    }

                    foreach (var domainContact in addRemoveItems.DomainContacts)
                    {
                        var contactElement = AddXmlElement(doc, add_remove_element, "domain:contact", domainContact.Id,
                            namespaceUri);

                        contactElement.SetAttribute("type", domainContact.Type);
                    }
                }

                if (addRemoveItems.Status != null &&
                    addRemoveItems.Status.Count > 0)
                {
                    if (add_remove_element == null)
                    {
                        add_remove_element = doc.CreateElement(tagName, namespaceUri);
                    }

                    foreach (var status in addRemoveItems.Status)
                    {
                        var statusElement = AddXmlElement(doc, add_remove_element, "domain:status", status.Value,
                            namespaceUri);

                        statusElement.SetAttribute("s", status.Type);

                        if (status.Lang != null)
                        {
                            statusElement.SetAttribute("lang", status.Lang);
                        }
                    }
                }
            }

            return add_remove_element;
        }

        public override DomainUpdateResponse FromBytes(byte[] bytes)
        {
            return new DomainUpdateResponse(bytes);
        }
    }
}