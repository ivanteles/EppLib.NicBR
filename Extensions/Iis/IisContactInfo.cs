#region

using System.Xml;
using EppLib.Entities;
using EppLib.Entities.Contact;

#endregion

namespace EppLib.Extensions.Iis
{
    public class IisContactInfo : ContactBase<IisContactInfoResponse>
    {
        private readonly string _id;

        public IisContactInfo(string id)
        {
            _id = id;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var contactInfo = BuildCommandElement(doc, "info", commandRootElement);

            AddXmlElement(doc, contactInfo, "contact:id", _id, NamespaceUri);

            return contactInfo;
        }

        public override IisContactInfoResponse FromBytes(byte[] bytes)
        {
            return new IisContactInfoResponse(bytes);
        }
    }
}