namespace EppLib.Entities.Contact
{
    public class ContactChange
    {
        public string Email;
        public Telephone Fax;
        public PostalInfo PostalInfo;
        public Telephone Voice;

        public ContactChange(Contact contact)
        {
            Email = contact.Email;
            Voice = contact.Voice;
            Fax = contact.Fax;

            PostalInfo = contact.PostalInfo;
        }

        public ContactChange()
        {
        }
    }
}