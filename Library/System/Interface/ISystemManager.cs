using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVista.System.Interface
{
    internal interface ISystemManager
    {
        #region PROPERTIES
        /// <summary>
        /// Returns formatted _hardwareID string captured by ManagementObjectSearcher.
        /// </summary>
        string HardwareID { get; }

        /// <summary>
        /// Returns formatted _processName string captured by ManagementObjectSearcher.
        /// </summary>
        string ProcessorName { get; }

        /// <summary>
        /// Returns the name of the machine captured by System.Environment.
        /// </summary>
        string EnvironmentMachineName { get; }

        /// <summary>
        /// Returns the name of the user captured by System.Environment.
        /// </summary>
        string EnvironmentUserName { get; }
        #endregion
    }
}
