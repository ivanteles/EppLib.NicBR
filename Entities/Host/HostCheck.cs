#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostCheck : HostBase<HostCheckResponse>
    {
        private readonly IList<string> _hosts;

        public HostCheck(string hostName)
        {
            _hosts = new List<string> {hostName};
        }

        public HostCheck(IList<string> hosts)
        {
            _hosts = hosts;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var hostCheck = BuildCommandElement(doc, "check", commandRootElement);

            foreach (var host in _hosts)
            {
                AddXmlElement(doc, hostCheck, "host:name", host, NamespaceUri);
            }

            return hostCheck;
        }

        public override HostCheckResponse FromBytes(byte[] bytes)
        {
            return new HostCheckResponse(bytes);
        }
    }
}