using DataVista.Database.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataVista.Database
{
    public class Connection : IConnection, IDisposable
    {
        #region FIELDS
        private bool _disposed = false;
        private SqlConnection? _sqlConnection;

        /// <summary>
        /// Configured using <see cref="ConfigurationManager"/>
        /// </summary>
        private string _connectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        #endregion

        #region CONSTRUCTOR & DESTRUCTOR
        public Connection(SqlConnection sqlConnection)
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = sqlConnection;
            }
            else
            {
                throw new DataException($"{this} failed to build ConnectionString");
            }
        }

        public Connection(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Destructor required to make the <see cref="Connection"/> of type <see cref="IDisposable"./>
        /// </summary>
        ~Connection()
        {
            _disposed = true;

            if (_sqlConnection != null)
            {
                _sqlConnection.Dispose();
            }

            Dispose(true);
        }
        #endregion

        #region PROPERTIES
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string WorkstationId
        {
            get
            {
                return _sqlConnection.WorkstationId;
            }
        }

        public SqlConnection SqlConnection
        {
            get
            {
                return _sqlConnection;
            }
        }

        public ConnectionState ConnectionState
        {
            get
            {
                return _sqlConnection.State;
            }
            set
            {
                if (_sqlConnection.State != value)
                {
                    if (value == ConnectionState.Open)
                    {
                        _sqlConnection.Open();
                    }
                }
                else
                {
                    if (_sqlConnection.State == ConnectionState.Closed)
                    {
                        _sqlConnection.Close();
                    }
                }
            }
        }

        public Guid ConnectionId
        {
            get
            {
                return _sqlConnection.ClientConnectionId;
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Requires:
        /// <para><see cref="SqlConnectionStringBuilder.DataSource"/>, <see cref="SqlConnectionStringBuilder.InitialCatalog"/></para>
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="InitialCatalog"></param>
        /// <returns></returns>
        public static string CreateConnectionString(string DataSource, string InitialCatalog)
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.IntegratedSecurity = true;
            stringBuilder.TrustServerCertificate = true;
            stringBuilder.PersistSecurityInfo = false;
            stringBuilder.InitialCatalog = InitialCatalog;
            stringBuilder.DataSource = DataSource;
            stringBuilder.Pooling = true;
            stringBuilder.MultipleActiveResultSets = true;

            return stringBuilder.ConnectionString;
        }

        public void Dispose()
        {
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.Dispose();
                        _sqlConnection = null;
                    }
                }
                _disposed = true;
            }
        }

        public override string ToString()
        {
            string connection = $"Connection is {ConnectionState}.";

            return connection;
        }
        #endregion
    }
}

