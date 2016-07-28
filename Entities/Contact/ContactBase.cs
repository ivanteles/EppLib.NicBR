#region

using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public abstract class ContactBase<T> : EppCommand<T> where T : EppResponse
    {
        protected ContactBase()
            : base("contact", "urn:ietf:params:xml:ns:contact-1.0")
        {
        }

        protected XmlElement AddressToXml(XmlDocument doc,
            string tagName,
            PostalInfo nameAddress)
        {
            XmlElement nameAddressElement = null;

            if (nameAddress != null)
            {
                nameAddressElement = doc.CreateElement(tagName, NamespaceUri);

                if (nameAddress.m_type == null)
                {
                    throw new EppException("missing the address type (postal info)");
                }

                nameAddressElement.SetAttribute("type", nameAddress.m_type);

                if (nameAddress.m_name != null)
                {
                    AddXmlElement(doc, nameAddressElement, "contact:name", nameAddress.m_name, NamespaceUri);
                }
                if (nameAddress.m_org != null)
                {
                    AddXmlElement(doc, nameAddressElement, "contact:org", nameAddress.m_org, NamespaceUri);
                }

                if (nameAddress.m_address != null)
                {
                    var address = nameAddress.m_address;
                    var addressElement = doc.CreateElement("contact:addr", NamespaceUri);
                    // Because this method is used by contact create and update,
                    // the lowest common denominator (update), says that all
                    // members are optional.
                    if (address.Street1 != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:street", address.Street1, NamespaceUri);
                    }
                    if (address.Street2 != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:street", address.Street2, NamespaceUri);
                    }
                    if (address.Street3 != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:street", address.Street3, NamespaceUri);
                    }
                    if (address.City != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:city", address.City, NamespaceUri);
                    }
                    if (address.StateProvince != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:sp", address.StateProvince, NamespaceUri);
                    }
                    if (address.PostalCode != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:pc", address.PostalCode, NamespaceUri);
                    }
                    if (address.CountryCode != null)
                    {
                        AddXmlElement(doc, addressElement, "contact:cc", address.CountryCode, NamespaceUri);
                    }

                    if (addressElement.ChildNodes.Count > 0)
                    {
                        nameAddressElement.AppendChild(addressElement);
                    }
                }
            }

            return nameAddressElement;
        }

        protected XmlElement DiscloseToXml(XmlDocument doc, Contact.DiscloseFlags discloseMask, bool discloseFlag)
        {
            var discloseElement = doc.CreateElement("contact:disclose", NamespaceUri);
            discloseElement.SetAttribute("flag", discloseFlag ? "1" : "0");

            var mask = discloseFlag ? discloseMask : ~discloseMask;

            if ((mask & Contact.DiscloseFlags.Voice) != 0)
            {
                AddXmlElement(doc, discloseElement, "contact:voice", null, NamespaceUri);
            }
            if ((mask & Contact.DiscloseFlags.Fax) != 0)
            {
                AddXmlElement(doc, discloseElement, "contact:fax", null, NamespaceUri);
            }
            if ((mask & Contact.DiscloseFlags.Email) != 0)
            {
                AddXmlElement(doc, discloseElement, "contact:email", null, NamespaceUri);
            }
            if ((mask & Contact.DiscloseFlags.NameInt) != 0)
            {
                var nameInt = AddXmlElement(doc, discloseElement, "contact:name", null, NamespaceUri);
                nameInt.SetAttribute("type", "int");
            }
            if ((mask & Contact.DiscloseFlags.NameLoc) != 0)
            {
                var nameLoc = AddXmlElement(doc, discloseElement, "contact:name", null, NamespaceUri);
                nameLoc.SetAttribute("type", "loc");
            }
            if ((mask & Contact.DiscloseFlags.OrganizationInt) != 0)
            {
                var orgInt = AddXmlElement(doc, discloseElement, "contact:org", null, NamespaceUri);
                orgInt.SetAttribute("type", "int");
            }
            if ((mask & Contact.DiscloseFlags.OrganizationLoc) != 0)
            {
                var orgLoc = AddXmlElement(doc, discloseElement, "contact:org", null, NamespaceUri);
                orgLoc.SetAttribute("type", "loc");
            }
            if ((mask & Contact.DiscloseFlags.AddressInt) != 0)
            {
                var addrInt = AddXmlElement(doc, discloseElement, "contact:addr", null, NamespaceUri);
                addrInt.SetAttribute("type", "int");
            }
            if ((mask & Contact.DiscloseFlags.AddressLoc) != 0)
            {
                var addrLoc = AddXmlElement(doc, discloseElement, "contact:addr", null, NamespaceUri);
                addrLoc.SetAttribute("type", "loc");
            }

            return discloseElement;
        }
    }
}