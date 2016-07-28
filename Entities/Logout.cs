#region

using System.Xml;

#endregion

namespace EppLib.Entities
{
    public class Logout : EppCommand<LogoutResponse>
    {
        protected override XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement)
        {
            var logout = CreateElement(doc, "logout");
            commandRootElement.AppendChild(logout);

            return commandRootElement;
        }

        public override LogoutResponse FromBytes(byte[] bytes)
        {
            return new LogoutResponse(bytes);
        }
    }
}