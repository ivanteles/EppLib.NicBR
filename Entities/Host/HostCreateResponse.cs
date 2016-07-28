#region

using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostCreateResponse : EppResponse
    {
        public HostCreateResponse(byte[] bytes)
            : base(bytes)
        {
        }

        protected HostCreateResult HostCreateResult { get; set; }

        protected override void ProcessDataNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("host", "urn:ietf:params:xml:ns:host-1.0");

            var children = doc.SelectSingleNode("/ns:epp/ns:response/ns:resData/host:creData", namespaces);

            if (children != null)
            {
                var hostNode = children.SelectSingleNode("host:name", namespaces);

                if (hostNode != null)
                {
                    HostCreateResult = new HostCreateResult {HostName = hostNode.InnerText};

                    var crDateNode = children.SelectSingleNode("host:crDate", namespaces);

                    if (crDateNode != null)
                    {
                        HostCreateResult.CreatedDate = crDateNode.InnerText;
                    }
                }
            }
        }
    }
}