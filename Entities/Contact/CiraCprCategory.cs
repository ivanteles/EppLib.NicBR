namespace EppLib.Entities.Contact
{
    public class CiraCprCategory
    {
        public CiraCprCategory(string cprCode, string cprDescription)
        {
            CprCode = cprCode;
            CprDescription = cprDescription;
        }

        public string CprCode { get; set; }
        public string CprDescription { get; set; }

        public override bool Equals(object obj)
        {
            if ((CiraCprCategory) obj == null) return false;

            return CprCode.Equals(((CiraCprCategory) obj).CprCode);
        }

        public override int GetHashCode()
        {
            if (CprCode == null) return 0;

            return CprCode.GetHashCode();
        }
    }
}