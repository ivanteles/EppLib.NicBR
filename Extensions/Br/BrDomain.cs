#region

using System.Xml;

#endregion

namespace EppLib.Extensions.Br
{
    public class BrDomain : BrDomainExtensionBase
    {
        private string _tipo ;

        public BrDomain(string tipo)
        {
            _tipo = tipo;
        }

        public BrDomain()
        {
        }
        public string Organization { get; set; }
        public int HasConcurrent { get; set; }
        public string TicketNumber { get; set; }
        public string AutoRenew { get; set; }
        public string ReleaseProcessFlags { get; set; }
        public string PublicationStatus { get; set; }


        public override XmlNode ToXml(XmlDocument doc)
        {
            switch (_tipo)
            {
                case "create":
                    var root = CreateElement(doc, "brdomain:create");
                    root.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:brdomain-1.0 brdomain-1.0.xsd");

                    if (!string.IsNullOrWhiteSpace(Organization))
                    {
                        AddXmlElement(doc, root, "brdomain:organization", Organization);
                    }

                    if (!string.IsNullOrWhiteSpace(ReleaseProcessFlags))
                    {
                        AddXmlElement(doc, root, "brdomain:releaseProcessFlags", "").SetAttribute("flag1", ReleaseProcessFlags);
                    }

                    if (!string.IsNullOrWhiteSpace(AutoRenew))
                    {
                        AddXmlElement(doc, root, "brdomain:autoRenew", "").SetAttribute("active", AutoRenew);
                    }

                    return root;
                case "check":
                    var rootCheck = CreateElement(doc, "brdomain:check");
                    rootCheck.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:brdomain-1.0 brdomain-1.0.xsd");

                    if (!string.IsNullOrWhiteSpace(Organization))
                    {
                        AddXmlElement(doc, rootCheck, "brdomain:organization", Organization);
                    }
                    return rootCheck;
                case "info":
                    var rootInfo = CreateElement(doc, "brdomain:info");
                    rootInfo.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:brdomain-1.0 brdomain-1.0.xsd");

                    if (!string.IsNullOrWhiteSpace(TicketNumber))
                    {
                        AddXmlElement(doc, rootInfo, "brdomain:ticketNumber", TicketNumber);
                    }
                    return rootInfo;
                case "update":
                    var rootUpdate = CreateElement(doc, "brdomain:update");
                    rootUpdate.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:brdomain-1.0 brdomain-1.0.xsd");

                    if (!string.IsNullOrWhiteSpace(TicketNumber))
                    {
                        AddXmlElement(doc, rootUpdate, "brdomain:ticketNumber", TicketNumber);
                    }

                    var elCd = AddXmlElement(doc, rootUpdate, "brorg:chg", "");

                    if (!string.IsNullOrWhiteSpace(ReleaseProcessFlags))
                    {
                        AddXmlElement(doc, elCd, "brdomain:releaseProcessFlags", "").SetAttribute("flag1", ReleaseProcessFlags);
                    }

                    if (!string.IsNullOrWhiteSpace(AutoRenew))
                    {
                        AddXmlElement(doc, elCd, "brdomain:autoRenew", "").SetAttribute("active", AutoRenew);
                    }

                    if (!string.IsNullOrWhiteSpace(PublicationStatus))
                    {
                        AddXmlElement(doc, rootUpdate, "brdomain:publicationStatus", PublicationStatus);
                    }

                    return rootUpdate;
                default:
                    return null;
            }
            
        }
    }
}