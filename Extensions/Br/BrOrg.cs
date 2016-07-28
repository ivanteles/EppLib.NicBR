#region

using System.Xml;

#endregion

namespace EppLib.Extensions.Br
{
    public class BrOrg : BrOrgExtensionBase
    {
        private string _tipo;

        public BrOrg()
        {

        }
        public BrOrg(string tipo)
        {
            _tipo = tipo;
        }
        public string Id { get; set; }
        public string Organization { get; set; }
        public string Contact { get; set; }
        public string ContactType { get; set; }
        public string Responsible { get; set; }


        public override XmlNode ToXml(XmlDocument doc)
        {
            switch (_tipo)
            {
                case "create":
                    var rootCreate = CreateElement(doc, "brorg:create");
                    //root.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:brorg-1.0 brorg-1.0.xsd");

                    if (!string.IsNullOrWhiteSpace(Organization))
                    {
                        AddXmlElement(doc, rootCreate, "brorg:organization", Organization);
                    }

                    if (!string.IsNullOrWhiteSpace(Contact))
                    {
                        var brOrgContact = AddXmlElement(doc, rootCreate, "brorg:contact", Contact);

                        if (!string.IsNullOrWhiteSpace(ContactType))
                        {
                            brOrgContact.SetAttribute("type", ContactType);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(Responsible))
                    {
                        AddXmlElement(doc, rootCreate, "brorg:responsible", Responsible);
                    }
                    return rootCreate;
                case "check":
                    var rootCheck = CreateElement(doc, "brorg:check");
                    //root.SetAttribute("xsi:schemaLocation", "urn:ietf:params:xml:ns:brorg-1.0 brorg-1.0.xsd");

                    var elCd = AddXmlElement(doc, rootCheck, "brorg:cd", "");

                    if (!string.IsNullOrWhiteSpace(Organization))
                    {
                        AddXmlElement(doc, elCd, "brorg:organization", Organization);
                    }

                    if (!string.IsNullOrWhiteSpace(Id))
                    {
                        AddXmlElement(doc, elCd, "brorg:id", Id);
                    }
                    return rootCheck;
                case "info":
                    var rootInfo = CreateElement(doc, "brorg:info");

                    if (!string.IsNullOrWhiteSpace(Organization))
                    {
                        AddXmlElement(doc, rootInfo, "brorg:organization", Organization);
                    }
                    return rootInfo;
                case "update":
                    var rootUpdate = CreateElement(doc, "brorg:update");

                    if (!string.IsNullOrWhiteSpace(Organization))
                    {
                        AddXmlElement(doc, rootUpdate, "brorg:organization", Organization);
                    }
                    return rootUpdate;
                default:
                    return null;
            }
            
        }
    }
}