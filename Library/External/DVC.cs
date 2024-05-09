using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataVista.Library.External
{
    /// <summary>
    /// Class importing the DataVista C dll functions.
    /// </summary>
    public class DVC
    {
        [DllImport("DVC.dll")]
        public static extern int Add(int a, int b);
    }
}
