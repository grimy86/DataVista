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
    public interface IConnection : IDisposable
    {
        #region PROPERTIES
        /// <summary>
        /// Returns a formatted connectionstring to connect to the database.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Returns a string that identifies the database client.
        /// Similar behaviour to EnvironmentUserName.
        /// </summary>
        string WorkstationId { get; }

        SqlConnection SqlConnection { get; }

        /// <summary>
        /// Returns a connectionstate to the database.
        /// Set the connection:
        ///     Closed = 0, Open = 1,
        ///     Connecting = 2, Executing = 4,
        ///     Fetching = 8,  Broken = 16
        /// </summary>
        ConnectionState ConnectionState { get; set; }

        /// <summary>
        /// Returns a connection ID of the most recent connection attempt,
        /// regardless of whether the attempt succeeded or failed.
        /// </summary>
        public Guid ConnectionId { get; }
        #endregion

        #region METHODS
        /// <summary>
        /// Let's the garbagecollector dispose of this object.
        /// </summary>
        public void Dispose();
        #endregion
    }
}
