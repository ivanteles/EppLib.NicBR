#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainCheck : DomainBase<DomainCheckResponse>
    {
        private readonly IList<string> _domains;

        public DomainCheck(string domain)
        {
            _domains = new List<string> {domain};
        }

        public DomainCheck(IList<string> domains)
        {
            _domains = domains;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainCheck = BuildCommandElement(doc, "check", commandRootElement);
            domainCheck.SetAttribute("xmlns:domain", "urn:ietf:params:xml:ns:domain-1.0");
            domainCheck.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:domain-1.0 domain-1.0.xsd");

            foreach (var domain in _domains)
            {
                AddXmlElement(doc, domainCheck, "domain:name", domain, NamespaceUri);
            }

            return domainCheck;
        }

        public override DomainCheckResponse FromBytes(byte[] bytes)
        {
            return new DomainCheckResponse(bytes);
        }
    }
}