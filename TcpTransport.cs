#region

using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml;

/**/

#endregion

/*
using OpenSSL;
using OpenSSL.Core;
using OpenSSL.X509;*/

namespace EppLib
{
    /// <summary>
    ///     Encapsulates the TCP transport
    /// </summary>
    public class TcpTransport : IDisposable
    {
        private readonly X509Certificate _clientCertificate;
        private readonly X509CertificateCollection _clientCertificateCollection;
        private readonly string _eppRegistryCom;
        private readonly bool _loggingEnabled;
        private readonly int _port;
        private readonly int _readTimeout;
        private readonly int _writeTimeout;
        private SslStream _stream;

        public TcpTransport(string host, int port, X509Certificate clientCertificate,
            X509CertificateCollection clientCertificateCollection, bool loggingEnabled = false,
            int readTimeout = Timeout.Infinite, int writeTimeout = Timeout.Infinite)
        {
            _eppRegistryCom = host;
            _port = port;
            _readTimeout = readTimeout;
            _writeTimeout = writeTimeout;
            _loggingEnabled = loggingEnabled;
            _clientCertificate = clientCertificate;
            _clientCertificateCollection = clientCertificateCollection;
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
            }
        }

        /// <summary>
        ///     Connect to the registry end point
        /// </summary>
        public void Connect(SslProtocols sslProtocols)
        {
            var client = new TcpClient(_eppRegistryCom, _port);

            _stream = new SslStream(client.GetStream(), false, ValidateServerCertificate)
            {
                ReadTimeout = _readTimeout,
                WriteTimeout = _writeTimeout
            };

            if (_clientCertificate != null)
            {
                var clientCertificates = new X509CertificateCollection {_clientCertificate};

                _stream.AuthenticateAsClient(_eppRegistryCom, clientCertificates, sslProtocols, false);
            }
            else
            {
                if (_clientCertificateCollection != null)
                {
                    _stream.AuthenticateAsClient(_eppRegistryCom, _clientCertificateCollection, sslProtocols, false);
                }
                else
                {
                    _stream.AuthenticateAsClient(_eppRegistryCom);
                }
            }
        }


        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        ///     Disconnect from the registry end point
        /// </summary>
        public void Disconnect()
        {
            _stream.Close();
        }

        /// <summary>
        ///     Read the command response
        /// </summary>
        /// <returns></returns>
        public byte[] Read()
        {
            var lenghtBytes = new byte[4];
            var read = 0;

            while (read < 4)
            {
                read = read + _stream.Read(lenghtBytes, read, 4 - read);
            }

            Array.Reverse(lenghtBytes);

            var length = BitConverter.ToInt32(lenghtBytes, 0) - 4;

            if (_loggingEnabled)
            {
                Debug.Log("Reading " + length + " bytes.");
            }

            var bytes = new byte[length];

            var returned = 0;

            while (returned != length)
            {
                returned += _stream.Read(bytes, returned, length - returned);
            }

            if (_loggingEnabled)
            {
                Debug.Log("****************** Received ******************");
                Debug.Log(bytes);
            }

            return bytes;
        }

        /// <summary>
        ///     Writes an XmlDocument to the transport stream
        /// </summary>
        /// <param name="s"></param>
        public void Write(XmlDocument s)
        {
            var bytes = GetBytes(s);

            var lenght = bytes.Length + 4;

            var lenghtBytes = BitConverter.GetBytes(lenght);
            Array.Reverse(lenghtBytes);

            _stream.Write(lenghtBytes, 0, 4);

            _stream.Write(bytes, 0, bytes.Length);
            _stream.Flush();

            if (_loggingEnabled)
            {
                Debug.Log("****************** Sending ******************");
                Debug.Log(bytes);
            }
        }

        private static byte[] GetBytes(XmlDocument s)
        {
            return Encoding.UTF8.GetBytes(s.OuterXml);
        }
    }
}