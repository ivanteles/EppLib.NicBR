#region

using System.Collections.Generic;

#endregion

namespace EppLib.Entities.Host
{
    public class Host
    {
        private readonly List<HostAddress> addresses = new List<HostAddress>();

        private readonly List<Status> status = new List<Status>();

        public Host(string hostName)
        {
            HostName = hostName;
        }

        public Host()
        {
        }

        public string HostName { get; set; }

        public string Roid { get; set; }

        public string ClId { get; set; }

        public string CrId { get; set; }

        public string CrDate { get; set; }

        public string UpId { get; set; }

        public string UpDate { get; set; }

        public string TrDate { get; set; }

        public IList<Status> Status
        {
            get { return status; }
        }

        public IList<HostAddress> Addresses
        {
            get { return addresses; }
        }
    }
}