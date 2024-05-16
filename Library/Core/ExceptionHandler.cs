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
    public static class ExceptionHandler
    {
        #region FIELDS
        internal static WinPath _winPath = new WinPath(WinPath.SpecialFolderType.MyDocuments);
        internal static string _folderPath = _winPath.Path + @"\Datavista\ExceptionHandler\";
        internal static string _filePath = _folderPath + "Logs.txt";
        #endregion

        /// <summary>
        /// Logs exceptions to <see cref="_filePath"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void LogException(MethodBase methodBase, Exception ex)
        {
            string logMessage = $"Time: {DateTime.Now}, Method: {methodBase.Name}, Error {ex.Message}{Environment.NewLine}" +
                $"@ StackTrace: {ex.StackTrace}, @Data {ex.Data}{Environment.NewLine}";

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
            }

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
