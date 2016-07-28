#region

using System.Xml;
using EppLib.Extensions.Br;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainCreateResponse : EppResponse
    {
        public DomainCreateResponse(string xml) : base(xml)
        {
        }

        public DomainCreateResponse(byte[] bytes) : base(bytes)
        {
        }

        public DomainCreateResult DomainCreateResult { get; set; }

        protected override void ProcessDataNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("domain", "urn:ietf:params:xml:ns:domain-1.0");

            var children = doc.SelectSingleNode("//domain:creData", namespaces);

            if (children != null)
            {
                var hostNode = children.SelectSingleNode("domain:name", namespaces);

                if (hostNode != null)
                {
                    DomainCreateResult = new DomainCreateResult {DomainName = hostNode.InnerText};

                    var crDateNode = children.SelectSingleNode("domain:crDate", namespaces);

                    if (crDateNode != null)
                    {
                        DomainCreateResult.CreatedDate = crDateNode.InnerText;
                    }

                    var exDateNode = children.SelectSingleNode("domain:exDate", namespaces);

                    if (exDateNode != null)
                    {
                        DomainCreateResult.ExpirationDate = exDateNode.InnerText;
                    }
                }
            }
        }

        protected override void ProcessExtensionNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("nom-direct-rights", "urn:ietf:params:xml:ns:domain-1.0");

            namespaces.AddNamespace("brdomain", "urn:ietf:params:xml:ns:brdomain-1.0");

            var creData = doc.SelectSingleNode("/ns:epp/ns:response/ns:extension/brdomain:creData", namespaces);
            if (creData != null)
            {
                DomainCreateResult.BrDomain = new BrDomain();

                var ticketNumber = creData.SelectSingleNode("brdomain:ticketNumber", namespaces);
                if (ticketNumber != null)
                {
                    DomainCreateResult.BrDomain.TicketNumber = ticketNumber.InnerText;
                }
            }
        }
    }
}