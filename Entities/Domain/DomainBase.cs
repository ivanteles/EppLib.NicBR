#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public abstract class DomainBase<T> : EppCommand<T> where T : EppResponse
    {
        protected DomainBase()
            : base("domain", "urn:ietf:params:xml:ns:domain-1.0")
        {
        }

        protected XmlNode CreateNameServerElement(XmlDocument doc, IEnumerable<HostAttr> nameServers)
        {
            var nameServerElement = doc.CreateElement("domain:ns", NamespaceUri);

            foreach (var serverName in nameServers)
            {
                var hostAttr = doc.CreateElement("domain:hostAttr", NamespaceUri);
                AddXmlElement(doc, hostAttr, "domain:hostName", serverName.HostName, NamespaceUri);
                if (!string.IsNullOrWhiteSpace(serverName.HostAddr))
                {
                    var el = AddXmlElement(doc, hostAttr, "domain:hostAddr", serverName.HostAddr, NamespaceUri);
                    el.SetAttribute("ip", serverName.HostAddrType);
                }
                nameServerElement.AppendChild(hostAttr);
            }

            return nameServerElement;
        }
    }
}