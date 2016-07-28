#region

using System;
using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainRenewResponse : EppResponse
    {
        protected DateTime? _exDate;

        public DomainRenewResponse(byte[] bytes) : base(bytes)
        {
        }

        public virtual DateTime? ExDate
        {
            get { return _exDate; }
        }

        protected override void ProcessDataNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("domain", "urn:ietf:params:xml:ns:domain-1.0");
            var renData = doc.SelectSingleNode("//domain:renData", namespaces);

            if (renData != null)
            {
                var exDateNode = renData.SelectSingleNode("domain:exDate", namespaces);

                if (exDateNode != null)
                {
                    DateTime exDate;
                    if (DateTime.TryParse(exDateNode.InnerText, out exDate))
                    {
                        _exDate = exDate;
                    }
                }
            }
        }
    }
}