#region

using System.Xml;

#endregion

namespace EppLib.Entities
{
    public class Login : EppCommand<LoginResponse>
    {
        private readonly string _clientId;
        private readonly string _newPassword;
        private readonly string _password;

        public Login(string clientId, string password, string newPassword)
        {
            _clientId = clientId;
            _password = password;
            _newPassword = newPassword;
        }

        public Login(string clientId, string password)
            : this(clientId, password, null)
        {
        }

        public Options Options { get; set; }
        public Services Services { get; set; }

        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var login = CreateElement(doc, "login");

            AddXmlElement(doc, login, "clID", _clientId);
            AddXmlElement(doc, login, "pw", _password);
            if (_newPassword != null) AddXmlElement(doc, login, "newPW", _newPassword);

            if (Options != null)
            {
                var optionsElement = CreateElement(doc, "options");

                if (Options.MVersion != null)
                {
                    AddXmlElement(doc, optionsElement, "version", Options.MVersion);
                }

                if (Options.MLang != null)
                {
                    AddXmlElement(doc, optionsElement, "lang", Options.MLang);
                }

                if (Options.NewPW != null)
                {
                    AddXmlElement(doc, optionsElement, "newPW", Options.NewPW);
                }

                login.AppendChild(optionsElement);
            }

            if (Services != null)
            {
                var svcsElement = CreateElement(doc, "svcs");

                foreach (var svc in Services.ObjUrIs)
                {
                    AddXmlElement(doc, svcsElement, "objURI", svc);
                }

                if (Services.Extensions != null)
                {
                    var svcExtension = CreateElement(doc, "svcExtension");

                    foreach (var extension in Services.Extensions)
                    {
                        AddXmlElement(doc, svcExtension, "extURI", extension);
                    }

                    svcsElement.AppendChild(svcExtension);
                }

                login.AppendChild(svcsElement);
            }

            commandRootElement.AppendChild(login);

            return commandRootElement;
        }

        public override LoginResponse FromBytes(byte[] bytes)
        {
            return new LoginResponse(bytes);
        }
    }

    public class ObjUri
    {
    }
}