#region

using System.Collections.Generic;

#endregion

namespace EppLib.Entities.Contact
{
    public class EppContactUpdateAddRemove
    {
        public IList<Status> Status = new List<Status>();
    }
}