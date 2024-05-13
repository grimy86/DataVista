using DataVista.SystemTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.Core
{
    internal class ExceptionHandler
    {
        #region FIELDS
        private static string? _winpath = SystemTools.WinPath.MyDocuments + @"\Datavista\ExceptionHandler\";
        private static string? _filePath = Path.Combine(_winpath, "Logs.txt");
        #endregion

        #region CONSTRUCTOR
        internal ExceptionHandler(MethodBase method, Exception ex)
        {
            if (!Directory.Exists(_winpath))
            {
                Directory.CreateDirectory(_winpath);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
            }

            LogException(method.Name, ex);
        }
        #endregion

        public static string? WinPath
        {
            get
            {
                return _winpath;
            }
            set
            {
                _winpath = value;
            }
        }

        /// <summary>
        /// Logs exceptions to <see cref="_filePath"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        internal void LogException(string handlerMessage, Exception ex)
        {
            string logMessage = $"Time: {DateTime.Now}, Handler: {handlerMessage}, Error {ex.Message}{Environment.NewLine}" +
                $"@ StackTrace: {ex.StackTrace}, @Data {ex.Data}{Environment.NewLine}";

            try
            {
                using (StreamWriter writer = File.AppendText(_filePath))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception logException)
            {
                Debug.WriteLine(logException);
            }
        }
    }
}
