namespace EppLib.Entities.Contact
{
    public class PostalInfo
    {
        public PostalAddress m_address;
        public string m_name;

        /// <summary>
        ///     If the contact name is an individual, an organization can be entered. Otherwise, it must be omitted.
        /// </summary>
        public string m_org;

        public string m_type;
    }
}