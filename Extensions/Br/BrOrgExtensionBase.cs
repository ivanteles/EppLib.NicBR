using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EppLib.Entities;

namespace EppLib.Extensions.Br
{
    public abstract class BrOrgExtensionBase : EppExtension
    {
        private string _ns = "urn:ietf:params:xml:ns:brorg-1.0";
        protected override string Namespace
        {
            get { return _ns; }
            set { _ns = value; }
        }
    }
}
