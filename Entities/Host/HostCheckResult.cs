#region

using System.Globalization;
using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostCheckResult
    {
        public HostCheckResult()
        {
        }

        public HostCheckResult(XmlNode child, XmlNamespaceManager namespaces)
        {
            var nameNode = child.SelectSingleNode("host:name", namespaces);

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

            var reasonNode = child.SelectSingleNode("host:reason", namespaces);

            if (reasonNode != null)
            {
                Reason = reasonNode.InnerText;
            }
        }

        public string Name { get; set; }
        public bool Available { get; set; }
        public string Reason { get; set; }

        public override bool Equals(object obj)
        {
            var o = (HostCheckResult) obj;

            if (o == null) return false;

            return (o.Name == Name) && (o.Available == Available) && (o.Reason == Reason);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Available.GetHashCode() ^ Reason.GetHashCode();
        }
    }
}