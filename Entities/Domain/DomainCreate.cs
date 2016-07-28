#region

using System.Collections.Generic;
using System.Globalization;
using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainCreate : DomainBase<DomainCreateResponse>
    {
        public DomainCreate(string domainName, string registrantContactId)
        {
            DomainName = domainName;
            RegistrantContactId = registrantContactId;
        }

        public string DomainName { get; set; }

        public DomainPeriod Period { get; set; }

        public string RegistrantContactId { get; set; }

        public IList<HostAttr> NameServers { get; } = new List<HostAttr>();

        public IList<DomainContact> DomainContacts { get; } = new List<DomainContact>();

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainCreate = BuildCommandElement(doc, "create", commandRootElement);
            domainCreate.SetAttribute("xmlns:domain", "urn:ietf:params:xml:ns:domain-1.0");
            domainCreate.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:domain-1.0 domain-1.0.xsd");

            AddXmlElement(doc, domainCreate, "domain:name", DomainName, NamespaceUri);

            if (Period != null)
            {
                var period = AddXmlElement(doc, domainCreate, "domain:period",
                    Period.Value.ToString(CultureInfo.InvariantCulture), NamespaceUri);

                period.SetAttribute("unit", Period.Unit);
            }

            if (NameServers != null && NameServers.Count > 0)
            {
                domainCreate.AppendChild(CreateNameServerElement(doc, NameServers));
            }

            if (RegistrantContactId != null)
            {
                AddXmlElement(doc, domainCreate, "domain:registrant", RegistrantContactId, NamespaceUri);
            }

            foreach (var contact in DomainContacts)
            {
                var contactElement = AddXmlElement(doc, domainCreate, "domain:contact", contact.Id, NamespaceUri);

                contactElement.SetAttribute("type", contact.Type);
            }

            return domainCreate;
        }

        public override DomainCreateResponse FromBytes(byte[] bytes)
        {
            return new DomainCreateResponse(bytes);
        }
    }
}