#region

using System.Security.Authentication;
using System.Xml;
using EppLib.Entities;

#endregion

namespace EppLib
{
    /// <summary>
    ///     Encapsulates the EPP protocol
    /// </summary>
    public class Service
    {
        private readonly TcpTransport _transport;

        public Service(TcpTransport transport)
        {
            _transport = transport;
        }


        /// <summary>
        ///     Connects to the registry end point
        /// </summary>
        public void Connect(SslProtocols sslProtocols = SslProtocols.Tls)
        {
            _transport.Connect(sslProtocols);
            _transport.Read();
        }

        /// <summary>
        ///     Executes an EPP command
        /// </summary>
        /// <param name="command">The EPP command</param>
        /// <returns></returns>
        public T Execute<T>(EppBase<T> command) where T : EppResponse
        {
            var bytes = SendAndReceive(command.ToXml());

            return command.FromBytes(bytes);
        }

        internal byte[] SendAndReceive(XmlDocument xmlDocument)
        {
            _transport.Write(xmlDocument);

            return _transport.Read();
        }

        /// <summary>
        ///     Disconects from the registry end point
        /// </summary>
        public void Disconnect()
        {
            _transport.Disconnect();
        }
    }
}