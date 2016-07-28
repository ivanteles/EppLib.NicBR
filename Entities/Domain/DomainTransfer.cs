#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainTransfer : DomainBase<DomainTransferResponse>
    {
        private readonly string _mName;
        protected string AdminContactId;
        protected string RegistrantContactId;
        protected IList<string> TechContactIds;

        public DomainTransfer(string mName)
        {
            _mName = mName;
        }

        public DomainTransfer(string mName, string registrantContactId, string adminContactId, string techContactId)
        {
            _mName = mName;
            TechContactIds = new List<string> {techContactId};
            AdminContactId = adminContactId;
            RegistrantContactId = registrantContactId;
        }

        public DomainTransfer(string mName, string registrantContactId, string adminContactId,
            IList<string> techContactIds)
        {
            _mName = mName;
            AdminContactId = adminContactId;
            TechContactIds = techContactIds;
            RegistrantContactId = registrantContactId;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainTransfer = BuildCommandElement(doc, "transfer", commandRootElement, "request");

            AddXmlElement(doc, domainTransfer, "domain:name", _mName, NamespaceUri);

            return domainTransfer;
        }

        public override DomainTransferResponse FromBytes(byte[] bytes)
        {
            return new DomainTransferResponse(bytes);
        }
    }
}