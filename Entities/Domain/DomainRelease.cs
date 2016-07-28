#region

using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainRelease : DomainBase<DomainReleaseResponse>
    {
        private readonly string _domainName;
        private readonly string _registrarTag;

        public DomainRelease(string domainName, string registrarTag)
        {
            _domainName = domainName;
            _registrarTag = registrarTag;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainRelease = BuildCommandElement(doc, "update", commandRootElement);

            var releaseNode = AddXmlElement(doc, domainRelease, "r:release", null,
                "http://www.nominet.org.uk/epp/xml/std-release-1.0");

            AddXmlElement(doc, releaseNode, "r:domainName", _domainName,
                "http://www.nominet.org.uk/epp/xml/std-release-1.0");
            AddXmlElement(doc, releaseNode, "r:registrarTag", _registrarTag,
                "http://www.nominet.org.uk/epp/xml/std-release-1.0");

            return domainRelease;
        }

        public override DomainReleaseResponse FromBytes(byte[] bytes)
        {
            return new DomainReleaseResponse(bytes);
        }

        private XmlElement BuildCommandElement(XmlDocument doc, string qualifiedName, XmlElement commandRootElement)
        {
            var elem = CreateElement(doc, qualifiedName);

            commandRootElement.AppendChild(elem);

            return elem;
        }
    }
}