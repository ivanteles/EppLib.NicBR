#region

using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainDelete : DomainBase<DomainDeleteResponse>
    {
        private readonly string _domainName;

        public DomainDelete(string domainName)
        {
            _domainName = domainName;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainDelete = BuildCommandElement(doc, "delete", commandRootElement);
            domainDelete.SetAttribute("xmlns:domain", "urn:ietf:params:xml:ns:domain-1.0");
            domainDelete.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:domain-1.0 domain-1.0.xsd");

            AddXmlElement(doc, domainDelete, "domain:name", _domainName, NamespaceUri);

            return domainDelete;
        }

        public override DomainDeleteResponse FromBytes(byte[] bytes)
        {
            return new DomainDeleteResponse(bytes);
        }
    }
}