#region

using System.Xml;

#endregion

namespace EppLib.Entities.Host
{
    public class HostUpdate : HostBase<HostUpdateResponse>
    {
        private readonly string _hostName;

        public HostChange HostChange;

        public EppHostUpdateAddRemove ToAdd;
        public EppHostUpdateAddRemove ToRemove;

        public HostUpdate(string hostName)
        {
            _hostName = hostName;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainUpdate = BuildCommandElement(doc, "update", commandRootElement);

            AddXmlElement(doc, domainUpdate, "host:name", _hostName, NamespaceUri);

            var addElement = GetAddRemoveElement(doc, ToAdd, "host:add", NamespaceUri);

            if (addElement != null)
            {
                domainUpdate.AppendChild(addElement);
            }

            var removeElement = GetAddRemoveElement(doc, ToRemove, "host:rem", NamespaceUri);

            if (removeElement != null)
            {
                domainUpdate.AppendChild(removeElement);
            }

            if (HostChange != null)
            {
                var changeElement = doc.CreateElement("host:chg", NamespaceUri);

                if (HostChange.HostName != null)
                {
                    AddXmlElement(doc, changeElement, "host:name", HostChange.HostName, NamespaceUri);
                }

                domainUpdate.AppendChild(changeElement);
            }

            return domainUpdate;
        }

        private static XmlElement GetAddRemoveElement(XmlDocument doc, EppHostUpdateAddRemove addRemoveItems,
            string tagName, string namespaceUri)
        {
            XmlElement addRemoveElement = null;

            if (addRemoveItems != null)
            {
                if (addRemoveItems.Adresses != null &&
                    addRemoveItems.Adresses.Count > 0)
                {
                    addRemoveElement = doc.CreateElement(tagName, namespaceUri);

                    foreach (var address in addRemoveItems.Adresses)
                    {
                        var addressElement = AddXmlElement(doc, addRemoveElement, "host:addr", address.IPAddress,
                            namespaceUri);

                        addressElement.SetAttribute("ip", address.IPVersion);
                    }
                }

                if (addRemoveItems.Status != null &&
                    addRemoveItems.Status.Count > 0)
                {
                    if (addRemoveElement == null)
                    {
                        addRemoveElement = doc.CreateElement(tagName, namespaceUri);
                    }

                    foreach (var status in addRemoveItems.Status)
                    {
                        var statusElement = AddXmlElement(doc, addRemoveElement, "host:status", status.Value,
                            namespaceUri);

                        statusElement.SetAttribute("s", status.Type);

                        if (status.Lang != null)
                        {
                            statusElement.SetAttribute("lang", status.Lang);
                        }
                    }
                }
            }


            return addRemoveElement;
        }

        public override HostUpdateResponse FromBytes(byte[] bytes)
        {
            return new HostUpdateResponse(bytes);
        }
    }
}