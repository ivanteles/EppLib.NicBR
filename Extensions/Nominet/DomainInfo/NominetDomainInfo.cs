using System;
using System.Xml;
using EppLib.Entities;
using EppLib.Entities.Domain;

namespace EppLib.Extensions.Nominet.DomainInfo
{
	public class NominetDomainInfo : DomainBase<NominetDomainInfoResponse>
	{
		private readonly string _domainName;

		public string Hosts { get; set; }

		public NominetDomainInfo(string domainName)
		{
			_domainName = domainName;
		}

		protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
		{
			var domainInfo = BuildCommandElement(doc, "info", commandRootElement);

			var domainNameElement = AddXmlElement(doc, domainInfo, "domain:name", _domainName, NamespaceUri);

			if (!String.IsNullOrEmpty(Hosts))
			{
				domainNameElement.SetAttribute("hosts", Hosts);
			}

			return domainInfo;
		}

		public override NominetDomainInfoResponse FromBytes(byte[] bytes)
		{
			return new NominetDomainInfoResponse(bytes);
		}
	}
}