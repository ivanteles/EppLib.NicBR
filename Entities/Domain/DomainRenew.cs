#region

using System;
using System.Globalization;
using System.Xml;

#endregion

namespace EppLib.Entities.Domain
{
    public class DomainRenew : DomainBase<DomainRenewResponse>
    {
        private readonly string _currentExpirationDate;
        private readonly string _domainName;
        private readonly DomainPeriod _mPeriod;

        public DomainRenew(string domainName, string currentExpirationDate, DomainPeriod mPeriod)
        {
            _domainName = domainName;
            _currentExpirationDate = currentExpirationDate;
            _mPeriod = mPeriod;
        }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var domainRenew = BuildCommandElement(doc, "renew", commandRootElement);

            AddXmlElement(doc, domainRenew, "domain:name", _domainName, NamespaceUri);
            AddXmlElement(doc, domainRenew, "domain:curExpDate",
                DateTime.Parse(_currentExpirationDate, CultureInfo.CurrentUICulture)
                    .ToString("yyyy-MM-dd", CultureInfo.CurrentUICulture), NamespaceUri);

            if (_mPeriod != null)
            {
                var period = AddXmlElement(doc, domainRenew, "domain:period",
                    _mPeriod.Value.ToString(CultureInfo.CurrentUICulture), NamespaceUri);
                period.SetAttribute("unit", _mPeriod.Unit);
            }

            return domainRenew;
        }

        public override DomainRenewResponse FromBytes(byte[] bytes)
        {
            return new DomainRenewResponse(bytes);
        }
    }
}