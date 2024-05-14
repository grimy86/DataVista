using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.Core
{
    /// <summary>
    /// Class importing dll functions.
    /// </summary>
    public class MemoryUtil
    {
        #region DVC
        [DllImport("DVC.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int* getAddressOfInt(int value);
        #endregion

        #region KERNEL32
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
         );
        #endregion
    }
}
