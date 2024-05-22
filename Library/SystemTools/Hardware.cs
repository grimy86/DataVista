using DataVista.SystemTools.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataVista.SystemTools
{
    public sealed class Hardware : ISystemManager
    {
        #region FIELDS
        private string? _hardwareID;
        private string? _processorName;
        #endregion

        #region CONSTRUCTOR
        public Hardware()
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
                if (_hardwareID != null)
                {
                    return _hardwareID;
                }
                else
                {
                    return _hardwareID = string.Empty;
                }
            }
        }

        public string ProcessorName
        {
            get
            {
                if (_processorName != null)
                {
                    return _processorName;
                }
                else
                {
                    return _processorName = string.Empty;
                }
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
        public override string ToString()
        {
            string environment =
                $"[HWID: {HardwareID}] {Environment.NewLine}" +
                $"[Processor: {ProcessorName}] {Environment.NewLine}" +
                $"[MachineName: {EnvironmentMachineName}] {Environment.NewLine}" +
                $"[Username: {EnvironmentUserName}] {Environment.NewLine}";

            return environment;
        }
        #endregion

        #region EXPERIMENTAL
        /*
                public static string GetRegistryKeys()
                {
                    string registryKeys = String.Empty;

                    return registryKeys;
                }

                public static string GetProcessHandles()
                {
                    Process[] runningProcesses = Process.GetProcesses();
                    HashSet<nint> uniqueHandles = new HashSet<nint>();
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (Process process in runningProcesses)
                    {
                        if (process.Id != 0 && process.Id != 4)
                        {
                            if (!uniqueHandles.Contains(process.Handle))
                            {
                                uniqueHandles.Add(process.Handle);

                                stringBuilder.AppendLine($"{process.Handle}");
                            }
                        }
                    }
                    return stringBuilder.ToString();
                }
        */
        #endregion
    }
}
