#region

using System;
using System.Collections;
using EppLib.Extensions.Br;

#endregion

namespace EppLib.Entities.Contact
{
    public class Contact
    {
        [Flags]
        public enum DiscloseFlags
        {
            All = ~0,
            None = 0,
            NameInt = 1,
            NameLoc = 2,
            OrganizationInt = 4,
            OrganizationLoc = 8,
            AddressInt = 16,
            AddressLoc = 32,
            Voice = 64,
            Fax = 128,
            Email = 256
        }

        [Obsolete("Use CIRA Extension", false)] public string AgreementTimestamp;

        [Obsolete("Use CIRA Extension", false)] public string CiraAgreementVersion;

        [Obsolete("Use CIRA Extension", false)] public string CprCategory;

        public string Email;
        public Telephone Fax;

        /// <summary>
        ///     Contact Id (3 - 16 characters)
        /// </summary>
        public string Id;

        [Obsolete("Use CIRA Extension", false)] public string Individual;

        [Obsolete("Use CIRA Extension", false)] public string Language;

        public PostalInfo PostalInfo;

        public Telephone Voice;

        [Obsolete("Use CIRA Extension", false)] public string WhoisDisplaySetting;

        public Contact(string contactId, string fullName, string companyName, string city, string street1,
            string street2, string street3, string province, string postalCode, string countryCode, string email,
            Telephone voice, Telephone fax)
        {
            var postalAddress = new PostalAddress
            {
                City = city,
                Street1 = street1,
                Street2 = street2,
                Street3 = street3,
                StateProvince = province,
                PostalCode = postalCode,
                CountryCode = countryCode
            };

            PostalInfo = new PostalInfo
            {
                m_name = fullName,
                m_org = companyName,
                m_type = PostalAddressType.LOC,
                m_address = postalAddress
            };

            Email = email;
            Voice = voice;
            Fax = fax;
            Id = contactId;
        }

        public Contact(string contactId, string fullName, string companyName, string city, string streetAddress,
            string province, string postalCode, string countryCode, string email, Telephone voice, Telephone fax)
            : this(
                contactId, fullName, companyName, city, streetAddress, null, null, province, postalCode, countryCode,
                email, voice, fax)
        {
        }

        public Contact()
        {
        }

        public string Roid { get; set; }

        [Obsolete("Use StatusList", false)]
        public string Status { get; set; }

        public IList StatusList { get; set; }
        public string ClId { get; set; }
        public string CrId { get; set; }
        public string UpId { get; set; }
        public string CrDate { get; set; }
        public string UpDate { get; set; }
        public string TrDate { get; set; }
        public string Password { get; set; }
        public bool? DiscloseFlag { get; set; }
        public DiscloseFlags DiscloseMask { get; set; }
        public BrOrg BrOrg { get; set; }
    }
}