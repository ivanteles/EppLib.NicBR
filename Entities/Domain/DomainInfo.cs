#region

using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainInfo : DomainBase<DomainInfoResponse>
    {
        private readonly string _domainName;

        public DomainInfo(string domainName)
        {
            _domainName = domainName;
        }

        /// <summary>
        ///     "all" (default if missing) request delegated and subordinate hosts.
        ///     "del" request delegeted hosts only.
        ///     "sub" request subordinate hosts only.
        ///     "none" request no information about hosts.
        /// </summary>
        public string Hosts { get; set; }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainInfo = BuildCommandElement(doc, "info", commandRootElement);
            domainInfo.SetAttribute("xmlns:domain", "urn:ietf:params:xml:ns:domain-1.0");
            domainInfo.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:domain-1.0 domain-1.0.xsd");

            var domainNameElement = AddXmlElement(doc, domainInfo, "domain:name", _domainName, NamespaceUri);

            if (!string.IsNullOrEmpty(Hosts))
            {
                domainNameElement.SetAttribute("hosts", Hosts);
            }

            return domainInfo;
        }

        public override DomainInfoResponse FromBytes(byte[] bytes)
        {
            return new DomainInfoResponse(bytes);
        }
    }
}