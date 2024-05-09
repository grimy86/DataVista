using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.Library.System
{
    public class WinProcess
    {
        /// <summary>
        /// This will only return a string containing <see cref="Process.Id"/> and <see cref="Process.ProcessName"/>
        /// <para>Format: <code>$"{process.Id} : {process.ProcessName}"</code></para>
        /// </summary>
        public static string UniqueProcesses
        {
            get { return GetUniqueProcesses(); }
        }

        public static Process[] RunningProcesses
        {
            get { return Process.GetProcesses(); }
        }

        private static string GetUniqueProcesses()
        {
            Process[] runningProcesses = Process.GetProcesses();
            HashSet<string> uniqueProcesses = new HashSet<string>();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Process process in runningProcesses)
            {
                if (!uniqueProcesses.Contains(process.ProcessName))
                {
                    uniqueProcesses.Add(process.ProcessName);

                    stringBuilder.AppendLine($"{process.Id} : {process.ProcessName}");
                }
            }
            return stringBuilder.ToString();
        }
    }
}
