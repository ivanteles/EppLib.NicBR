namespace EppLib.Entities.Contact
{
    public class Telephone
    {
        public string Extension;
        public string Value;

        public Telephone(string value, string extension)
        {
            Value = value;
            Extension = extension;
        }

        public Telephone()
        {
        }
    }
}