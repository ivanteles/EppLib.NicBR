#region

using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostCreate : HostBase<HostCreateResponse>
    {
        public HostCreate(Host host)
        {
            Host = host;
        }

        public Host Host { get; set; }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var hostCreate = BuildCommandElement(doc, "create", commandRootElement);

            AddXmlElement(doc, hostCreate, "host:name", Host.HostName, NamespaceUri);

            foreach (var address in Host.Addresses)
            {
                var node = AddXmlElement(doc, hostCreate, "host:addr", address.IPAddress, NamespaceUri);

                node.SetAttribute("ip", address.IPVersion);
            }

            return hostCreate;
        }

        public override HostCreateResponse FromBytes(byte[] bytes)
        {
            return new HostCreateResponse(bytes);
        }
    }
}