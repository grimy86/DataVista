using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataVista.SystemTools
{
    public class WinProcess
    {
        /// <summary>
        /// This will only return a string containing <see cref="Process.Id"/> and <see cref="Process.ProcessName"/>
        /// <para>Format: <code>$"{process.Id} : {process.ProcessName}"</code></para>
        /// </summary>
        /// 
        public static string UniqueProcesses
        {
            get { return GetUniqueProcesses(RunningProcesses); }
        }

        public static int RunningProcessesCount
        {
            get { return Process.GetProcesses().Length; }
        }

        public static Process[] RunningProcesses
        {
            get { return Process.GetProcesses(); }
        }

        private static string GetUniqueProcesses(Process[] processes)
        {
            HashSet<int> uniqueProcesses = new HashSet<int>();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Process process in processes)
            {
                if (!uniqueProcesses.Contains(process.Id))
                {
                    uniqueProcesses.Add(process.Id);

                    stringBuilder.AppendLine($"{process.Id} : {process.ProcessName}");
                }
            }
            return stringBuilder.ToString();
        }

        public static string GetUniqueThreads(Process process, ProcessThreadCollection threads)
        {
            HashSet<int> uniqueThreads = new HashSet<int>();
            StringBuilder stringBuilder = new StringBuilder();
            int BasePriority;

            foreach (ProcessThread thread in threads)
            {
                if (!uniqueThreads.Contains(thread.Id))
                {
                    uniqueThreads.Add(thread.Id);
                    BasePriority = process.BasePriority - thread.BasePriority;

                    stringBuilder.AppendLine($"Thread ID: {thread.Id}");
                    stringBuilder.AppendLine($"Thread Container: {thread.Container}");
                    stringBuilder.AppendLine($"Thread StartAddress {thread.StartAddress}");
                    switch (BasePriority)
                    {
                        case 0:
                            stringBuilder.AppendLine($"Thread Priority: Lowest");
                            break;
                        case 1:
                            stringBuilder.AppendLine($"Thread Priority: BelowNormal");
                            break;
                        case 2:
                            stringBuilder.AppendLine($"Thread Priority: Normal");
                            break;
                        case 3:
                            stringBuilder.AppendLine($"Thread Priority: AboveNormal");
                            break;
                        case 4:
                            stringBuilder.AppendLine($"Thread Priority: Highest");
                            break;
                        default:
                            stringBuilder.AppendLine($"Thread Priority: {thread.BasePriority}");
                            break;
                    }
                    stringBuilder.AppendLine(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }

        public static string GetProcessInfo(Process process)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Process Name: {process.ProcessName}");
            stringBuilder.AppendLine($"Process ID: {process.Id}");
            switch (process.BasePriority)
            {
                case 4:
                    stringBuilder.AppendLine($"Process Priority: Idle");
                    break;
                case 8:
                    stringBuilder.AppendLine($"Process Priority: Normal");
                    break;
                case 13:
                    stringBuilder.AppendLine($"Process Priority: High");
                    break;
                case 24:
                    stringBuilder.AppendLine($"Process Priority: RealTime");
                    break;
                default:
                    stringBuilder.AppendLine($"Process Priority: Unknown");
                    break;
            }
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendLine(GetUniqueThreads(process, process.Threads));

            return stringBuilder.ToString();
        }
    }
}
