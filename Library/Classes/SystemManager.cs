using DataVista.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataVista.Library.Classes
{
    public class SystemManager : ISystemManager
    {
        #region FIELDS
        private string? _hardwareID;
        private string? _processorName;
        #endregion

        #region CONSTRUCTOR
        public SystemManager()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {
                foreach (ManagementObject managementObject in searcher.Get())
                {
                    _hardwareID = managementObject["ProcessorId"].ToString().Trim();
                    _processorName = managementObject["Name"].ToString().TrimEnd();
                }
            }
        }
        #endregion

        #region PROPERTIES
        public string HardwareID
        {
            get
            {
                return _hardwareID;
            }
        }

        public string ProcessorName
        {
            get
            {
                return _processorName;
            }
        }

        public string EnvironmentMachineName
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public string EnvironmentUserName
        {
            get
            {
                return Environment.UserName;
            }
        }
        #endregion

        #region METHODS
        public static string GetUniqueProcesses()
        {
            Process[] runningProcesses = Process.GetProcesses();
            HashSet<string> uniqueProcesses = new HashSet<string>();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Process process in runningProcesses)
            {
                if (!uniqueProcesses.Contains(process.ProcessName))
                {
                    uniqueProcesses.Add(process.ProcessName);

                    stringBuilder.AppendLine($"{process.Id}  :  {process.ProcessName}");
                }
            }
            return stringBuilder.ToString();
        }

        public string GetMachineID()
        {
            string environment =
                $"[HWID: {HardwareID}] {Environment.NewLine}" +
                $"[Processor: {ProcessorName}] {Environment.NewLine}" +
                $"[MachineName: {EnvironmentMachineName}] {Environment.NewLine}" +
                $"[Username: {EnvironmentUserName}] {Environment.NewLine}";

            return environment;
        }
        #endregion
    }
}
