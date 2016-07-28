#region

using System.Xml;

#endregion

namespace EppLib.Extensions.Iis
{
    public class IisContactCreateExtension : IisExtensionBase
    {
        public string OrganizationNumber { get; set; }
        public string VatNumber { get; set; }

        public override XmlNode ToXml(XmlDocument doc)
        {
            var root = CreateElement(doc, "iis:create");

            if (!string.IsNullOrWhiteSpace(OrganizationNumber))
            {
                AddXmlElement(doc, root, "iis:orgno", OrganizationNumber);
            }

            if (!string.IsNullOrWhiteSpace(VatNumber))
            {
                AddXmlElement(doc, root, "iis:vatno", VatNumber);
            }

            return root;
        }
    }
}