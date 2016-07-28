#region

using System.Globalization;
using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactCheckResult
    {
        public ContactCheckResult()
        {
        }

        public ContactCheckResult(XmlNode child, XmlNamespaceManager namespaces)
        {
            var idNode = child.SelectSingleNode("contact:id", namespaces);

            if (idNode != null)
            {
                Id = idNode.InnerText;

                if (idNode.Attributes != null)
                {
                    var xmlAttribute = idNode.Attributes["avail"];

                    if (xmlAttribute != null)
                    {
                        var attributeValue = xmlAttribute.Value.ToLower(CultureInfo.InvariantCulture);
                        Available = attributeValue.Equals("true") || attributeValue.Equals("1");
                    }
                }
            }

            var reasonNode = child.SelectSingleNode("contact:reason", namespaces);

            if (reasonNode != null)
            {
                Reason = reasonNode.InnerText;
            }
        }

        public string Id { get; set; }
        public bool Available { get; set; }
        public string Reason { get; set; }

        public override bool Equals(object obj)
        {
            var o = (ContactCheckResult) obj;

            if (o == null) return false;

            return (o.Id == Id) && (o.Available == Available) && (o.Reason == Reason);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Available.GetHashCode() ^ Reason.GetHashCode();
        }
    }
}