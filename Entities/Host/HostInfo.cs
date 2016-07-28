#region

using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostInfo : HostBase<HostInfoResponse>
    {
        private readonly string _hostName;

        public HostInfo(string hostName)
        {
            _hostName = hostName;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var hostInfo = BuildCommandElement(doc, "info", commandRootElement);

            AddXmlElement(doc, hostInfo, "host:name", _hostName, NamespaceUri);

            return hostInfo;
        }

        public override HostInfoResponse FromBytes(byte[] bytes)
        {
            return new HostInfoResponse(bytes);
        }
    }
}