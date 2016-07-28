#region

using System.Xml;
using EppLib.Entities;
using EppLib.Entities.Domain;

#endregion

namespace EppLib.Extensions.Iis
{
    public class IisDomainInfo : DomainBase<IisDomainInfoResponse>
    {
        private readonly string _domainName;

        public IisDomainInfo(string domainName)
        {
            _domainName = domainName;
        }

        public string Hosts { get; set; }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainInfo = BuildCommandElement(doc, "info", commandRootElement);

            var domainNameElement = AddXmlElement(doc, domainInfo, "domain:name", _domainName, NamespaceUri);

            if (!string.IsNullOrEmpty(Hosts))
            {
                domainNameElement.SetAttribute("hosts", Hosts);
            }

            return domainInfo;
        }

        public override IisDomainInfoResponse FromBytes(byte[] bytes)
        {
            return new IisDomainInfoResponse(bytes);
        }
    }
}