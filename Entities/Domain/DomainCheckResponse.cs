#region

using System.Collections.Generic;
using System.Xml;
using EppLib.Extensions.Br;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainCheckResponse : EppResponse
    {
        private readonly List<DomainCheckResult> results = new List<DomainCheckResult>();

        public DomainCheckResponse(byte[] bytes)
            : base(bytes)
        {
        }

        public IList<DomainCheckResult> Results
        {
            get { return results; }
        }

        protected override void ProcessDataNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("domain", "urn:ietf:params:xml:ns:domain-1.0");

            var children = doc.SelectNodes("/ns:epp/ns:response/ns:resData/domain:chkData/domain:cd", namespaces);

            if (children != null)
            {
                foreach (XmlNode child in children)
                {
                    results.Add(new DomainCheckResult(child, namespaces));
                }
            }
        }

        //Modified by Luke Dobson Fasthosts
        //handle the Reserved flags (nom-direct-rights) in the Extension nodes
        protected override void ProcessExtensionNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("nom-direct-rights", "urn:ietf:params:xml:ns:domain-1.0");

            var child1 = doc.SelectSingleNode("/ns:epp/ns:response/ns:extension", namespaces);

            if (child1 != null && !string.IsNullOrEmpty(child1.InnerText))
            {
                results[0].Reserved = child1.InnerText;
            }

            namespaces.AddNamespace("brdomain", "urn:ietf:params:xml:ns:brdomain-1.0");

            var infData = doc.SelectSingleNode("/ns:epp/ns:response/ns:extension/brdomain:chkData", namespaces);
            if (infData != null)
            {
                results[0].BrDomain = new BrDomain();

                var cd = infData.SelectSingleNode("brdomain:cd", namespaces);
                if (cd != null)
                {
                    if (cd.Attributes["hasConcurrent"] != null)
                    {
                        results[0].BrDomain.HasConcurrent = int.Parse(cd.Attributes["hasConcurrent"].Value);
                    }
                }

                var ticketNumber = cd.SelectSingleNode("brdomain:ticketNumber", namespaces);
                if (ticketNumber != null)
                {
                    results[0].BrDomain.TicketNumber = ticketNumber.InnerText;
                }
            }
        }
    }
}