#region

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;

#endregion

namespace EppLib
{
    internal class SimpleLogger : IDebugger
    {
        public static string LogFilename = Path.Combine(HttpRuntime.AppDomainAppPath, "bin\\epplog.txt");

        public void Log(byte[] bytes)
        {
            LogMessageToFile(Encoding.UTF8.GetString(bytes));
        }

        public void Log(string str)
        {
            LogMessageToFile(str);
        }

        private static void LogMessageToFile(string msg)
        {
            var sw = File.AppendText(LogFilename);

            try
            {
                var logLine = string.Format(CultureInfo.InvariantCulture, "{0:G}: {1}.", DateTime.Now, msg);

                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}