#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace EppLib.Entities
{
    public abstract class EppCommand<T> : EppBase<T> where T : EppResponse
    {
        public readonly IList<EppExtension> Extensions = new List<EppExtension>();
        protected readonly string NamespaceUri;
        protected readonly string Nspace;

        protected EppCommand(string nspace, string namespaceUri)
        {
            Nspace = nspace;
            NamespaceUri = namespaceUri;
        }

        protected EppCommand()
        {
        }

        /// <summary>
        ///     Length is 6 - 16, ascii chars
        /// </summary>
        public string Password { get; set; }

        public string TransactionId { get; set; }

        public override XmlDocument ToXml()
        {
            var doc = new XmlDocument();
            var commandRootElement = GetCommandRootElement(doc);

            var cmdElement = BuildCommandElement(doc, commandRootElement);

            AppendAuthInfo(doc, cmdElement);

            AppendTRID(doc, commandRootElement);

            return doc;
        }

        private void AppendAuthInfo(XmlDocument doc, XmlElement cmdElement)
        {
            if (!string.IsNullOrWhiteSpace(Password))
            {
                var authInfo = AddXmlElement(doc, cmdElement, Nspace + ":authInfo", null, NamespaceUri);
                AddXmlElement(doc, authInfo, Nspace + ":pw", Password, NamespaceUri);
            }
        }

        private void SetCommonAttributes(XmlElement command)
        {
            command.SetAttribute("xmlns:" + Nspace, NamespaceUri);
        }

        protected XmlElement BuildCommandElement(XmlDocument doc, string qualifiedName, XmlElement commandRootElement,
            string query = null)
        {
            var command = GetCommand(doc, qualifiedName, commandRootElement, query);

            if (Extensions != null)
            {
                PrepareExtensionElement(doc, commandRootElement, Extensions);
            }

            return command;
        }

        private XmlElement GetCommand(XmlDocument doc, string qualifiedName, XmlElement commandRootElement,
            string query = null)
        {
            var elem = CreateElement(doc, qualifiedName);

            if (query != null)
            {
                elem.SetAttribute("op", query);
            }

            var command = CreateElement(doc, Nspace + ":" + qualifiedName);

            SetCommonAttributes(command);

            elem.AppendChild(command);

            commandRootElement.AppendChild(elem);

            return command;
        }

        protected static XmlElement GetCommandRootElement(XmlDocument doc)
        {
            var root = CreateDocRoot(doc);

            doc.AppendChild(root);

            var command = CreateElement(doc, "command");

            root.AppendChild(command);

            return command;
        }

        protected void AppendTRID(XmlDocument doc, XmlNode command)
        {
            if (string.IsNullOrWhiteSpace(TransactionId)) return;

            var clTRIDElement = CreateElement(doc, "clTRID");

            clTRIDElement.InnerText = TransactionId;

            command.AppendChild(clTRIDElement);
        }

        protected abstract XmlElement BuildCommandElement(XmlDocument doc, XmlElement commandRootElement);
    }
}