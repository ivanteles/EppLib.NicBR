#region

using EppLib.Extensions.Br;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainCreateResult
    {
        public string DomainName { get; set; }
        public string CreatedDate { get; set; }
        public string ExpirationDate { get; set; }
        public BrDomain BrDomain { get; set; }
    }
}