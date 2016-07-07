using System;
using System.IO;

namespace KTM.RevitSlack.Utils
{
    class Logger
    {
        private static string _fileName;

        /// <summary>
        /// Write Log message to log file
        /// </summary>
        /// <param name="message">text to write</param>
        /// <param name="ex">optional: pass exception (not handled right now)</param>
        public static void WriteLine(string message, Exception ex = null)
        {
            if (!File.Exists(_fileName))
                _fileName = GetFilename("KTM.RevitSlack");

            string m_logText = String.Format("{0} : \t{1}",
              DateTime.Now.ToLongTimeString(),
              message
              );

            File.AppendAllText(_fileName, m_logText + Environment.NewLine);

        }

        /// <summary>
        /// Get or Create the Log file
        /// </summary>
        /// <returns>path of log file</returns>
        private static string GetFilename(string logfilename)
        {
            return Path.Combine(Path.GetTempPath(),
              String.Format("{0}_{1}_{2}.log",
                logfilename,
                clsAppVersionHelpers.RevitVersion,
                DateTime.Now.Ticks));
        }
    }
}
