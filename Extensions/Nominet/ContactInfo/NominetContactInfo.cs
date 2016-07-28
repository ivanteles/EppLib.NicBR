#region

using System.Xml;
using EppLib.Entities;
using EppLib.Entities.Contact;

#endregion

namespace EppLib.Extensions.Nominet.ContactInfo
{
    public class NominetContactInfo : ContactBase<NominetContactInfoResponse>
    {
        private readonly string _mId;

        public NominetContactInfo(string mId)
        {
            _mId = mId;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var contactInfo = BuildCommandElement(doc, "info", commandRootElement);

            AddXmlElement(doc, contactInfo, "contact:id", _mId, NamespaceUri);

            return contactInfo;
        }

        public override NominetContactInfoResponse FromBytes(byte[] bytes)
        {
            return new NominetContactInfoResponse(bytes);
        }
    }
}