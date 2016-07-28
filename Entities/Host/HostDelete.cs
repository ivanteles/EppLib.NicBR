#region

using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostDelete : HostBase<HostDeleteResponse>
    {
        protected string HostName { get; set; }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var hostInfo = BuildCommandElement(doc, "delete", commandRootElement);

            AddXmlElement(doc, hostInfo, "host:name", HostName, NamespaceUri);

            return hostInfo;
        }

        public override HostDeleteResponse FromBytes(byte[] bytes)
        {
            return new HostDeleteResponse(bytes);
        }
    }
}