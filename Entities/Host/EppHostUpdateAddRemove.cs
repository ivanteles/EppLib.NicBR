#region

using System.Collections.Generic;

#endregion

namespace EppLib.Entities.Host
{
    public class EppHostUpdateAddRemove
    {
        public IList<Status> Status;
        public IList<HostAddress> Adresses { get; set; }
    }
}