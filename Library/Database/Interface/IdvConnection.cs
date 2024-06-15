using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataVista.Database.Interface
{
    public interface IdvConnection : IDisposable
    {
        #region PROPERTIES
        /// <summary>
        /// Returns a formatted connectionstring to connect to the database.
        /// </summary>
        string ConnectionString { get; }

        SqlConnection SqlConnection { get; }

        /// <summary>
        /// Returns a connectionstate to the database.
        /// Set the connection:
        ///     Closed = 0, Open = 1,
        ///     Connecting = 2, Executing = 4,
        ///     Fetching = 8,  Broken = 16
        /// </summary>
        ConnectionState ConnectionState { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Lets the garbagecollector dispose of this object.
        /// </summary>
        public void Dispose();
        #endregion
    }
}
