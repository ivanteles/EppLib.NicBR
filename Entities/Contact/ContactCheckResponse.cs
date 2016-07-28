#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities.Contact
{
    public class ContactCheckResponse : EppResponse
    {
        private readonly List<ContactCheckResult> _results = new List<ContactCheckResult>();

        public ContactCheckResponse(byte[] bytes) : base(bytes)
        {
        }

        public IList<ContactCheckResult> Results
        {
            get { return _results; }
        }

        protected override void ProcessDataNode(XmlDocument doc, XmlNamespaceManager namespaces)
        {
            namespaces.AddNamespace("contact", "urn:ietf:params:xml:ns:contact-1.0");

            var children = doc.SelectNodes("/ns:epp/ns:response/ns:resData/contact:chkData/contact:cd", namespaces);
            if (children != null)
            {
                foreach (XmlNode child in children)
                {
                    _results.Add(new ContactCheckResult(child, namespaces));
                }
            }
        }
    }
}