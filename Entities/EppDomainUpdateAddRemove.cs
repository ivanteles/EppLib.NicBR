#region

using System.Collections.Generic;
using EppLib.Entities.Domain;

#endregion

namespace EppLib.Entities
{
    public class EppDomainUpdateAddRemove
    {
        public IList<DomainContact> DomainContacts = new List<DomainContact>();
        public IList<HostAttr> NameServers = new List<HostAttr>();
        public IList<Status> Status = new List<Status>();
    }
}