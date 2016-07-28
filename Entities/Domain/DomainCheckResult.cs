#region

using System.Globalization;
using System.Xml;
using EppLib.Extensions.Br;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainCheckResult
    {
        public DomainCheckResult()
        {
        }

        public DomainCheckResult(XmlNode child, XmlNamespaceManager namespaces)
        {
            var nameNode = child.SelectSingleNode("domain:name", namespaces);

            if (nameNode != null)
            {
                Name = nameNode.InnerText;

                if (nameNode.Attributes != null)
                {
                    var xmlAttribute = nameNode.Attributes["avail"];

                    if (xmlAttribute != null)
                    {
                        var attributeValue = xmlAttribute.Value.ToLower(CultureInfo.InvariantCulture);
                        Available = attributeValue.Equals("true") || attributeValue.Equals("1");
                    }
                }
            }

            var reasonNode = child.SelectSingleNode("domain:reason", namespaces);

            if (reasonNode != null)
            {
                Reason = reasonNode.InnerText;
            }
        }

        public string Name { get; set; }
        public bool Available { get; set; }
        public string Reason { get; set; }
        public string Reserved { get; set; }

        public BrDomain BrDomain { get; set; }

        public override bool Equals(object obj)
        {
            var o = (DomainCheckResult) obj;

            if (o == null) return false;

            return (o.Name == Name) && (o.Available == Available) && (o.Reason == Reason);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Available.GetHashCode() ^ Reason.GetHashCode();
        }
    }
}