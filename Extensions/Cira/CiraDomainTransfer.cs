#region

using System.Collections.Generic;
using System.Xml;
using EppLib.Entities;
using EppLib.Entities.Domain;

#endregion

namespace EppLib.Extensions.Cira
{
    public class CiraDomainTransfer : DomainTransfer
    {
        public CiraDomainTransfer(string mName) : base(mName)
        {
        }

        public CiraDomainTransfer(string mName, string registrantContactId, string adminContactId, string techContactId)
            : base(mName, registrantContactId, adminContactId, techContactId)
        {
        }

        public CiraDomainTransfer(string mName, string registrantContactId, string adminContactId,
            IList<string> techContactIds) : base(mName, registrantContactId, adminContactId, techContactIds)
        {
        }

        public override XmlDocument ToXml()
        {
            var ciraExtension = new CiraTransferExtension
            {
                RegistrantContactId = RegistrantContactId,
                AdminContactId = AdminContactId,
                TechContactIds = TechContactIds
            };

            Extensions.Clear();
            Extensions.Add(ciraExtension);

            return base.ToXml();
        }
    }
}