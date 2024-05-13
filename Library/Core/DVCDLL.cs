using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.Core
{
    /// <summary>
    /// Class importing the DataVista C dll functions.
    /// </summary>
    public class DVCDLL
    {
        #region EXPERIMENTAL / UNSAFE
        [DllImport("address_of_int.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int* getAddressOfInt(int value);
        #endregion
    }
}
