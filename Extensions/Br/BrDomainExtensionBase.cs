using EppLib.Entities;

namespace EppLib.Extensions.Br
{
    public abstract class BrDomainExtensionBase : EppExtension
    {
        private string _ns = "urn:ietf:params:xml:ns:brdomain-1.0";
        protected override string Namespace
        {
            get { return _ns; }
            set { _ns = value; }
        }
    }
}
