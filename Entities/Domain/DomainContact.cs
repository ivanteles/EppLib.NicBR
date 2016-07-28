namespace EppLib.Entities.Domain
{
    public class DomainContact
    {
        public readonly string Id;

        /// <summary>
        ///     tech, admin, billing
        /// </summary>
        public readonly string Type;

        public DomainContact(string id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}