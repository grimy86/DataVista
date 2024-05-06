using DataVista.Database.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVista.Database
{
    public class Operation
    {
        #region FIELDS
        private readonly IConnection _Connection;
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Pass a connectionhandler to use for DbOperations
        /// <para>ConnectionHandler requires a SqlConnection !</para>
        /// <see cref="IConnection"/>
        /// </summary>
        /// <param name="connectionHandler">The IDatabaseConnection implementation to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if connectionHandler is null.</exception>
        public Operation(IConnection Connection)
        {
            if (Connection != null)
            {
                _Connection = Connection;
            }
            else
            {
                throw new ArgumentNullException(nameof(Connection));
            }
        }

        /// <summary>
        /// Implements a default SqlConnection and IDbConnectionHandler type.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public Operation()
        {
            SqlConnection sqlConnection = new SqlConnection();
            IConnection Connection = new Connection(sqlConnection);

            if (Connection != null && Connection.ConnectionString != null)
            {
                _Connection = Connection;
            }
            else
            {
                throw new ArgumentNullException(nameof(Connection));
            }
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Returns a <see cref="IConnection"/>.
        /// </summary>
        public IConnection Connection
        {
            get
            {
                return _Connection;
            }
        }
        #endregion

        #region T SqlOperation<T> DELEGATE
        public delegate T SqlOperation<T>(string query);

        public T ExecuteSQL<T>(string query, SqlOperation<T> sqlOperation)
        {
            return sqlOperation(query);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Execute a stored procedure by passing the name of the procedure.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public DataTable ExecuteProcedure(string procedureName)
        {
            DataTable dataTable = new DataTable();

            using (_Connection.SqlConnection)
            {
                _Connection.SqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = _Connection.SqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = procedureName;

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    dataTable.Load(dataReader);
                }
            }
            _Connection.SqlConnection.Close();
            return dataTable;
        }

        /// <summary>
        /// Execute a stored procedure with parameters by passing the name of the procedure and a parameter with a value.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable ExecuteParameterizedProcedure(string procedureName, string parameterName, object value)
        {
            DataTable dataTable = new DataTable();
            SqlParameter sqlParameter = new SqlParameter(parameterName, value);

            using (_Connection.SqlConnection)
            {
                _Connection.SqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = _Connection.SqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = procedureName;
                sqlCommand.Parameters.AddWithValue(parameterName, value);

                using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                {
                    dataTable.Load(dataReader);
                }
                _Connection.SqlConnection.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Example:
        ///<para>string query = "SELECT * FROM TableName";</para>
        ///<para>DataTable TableName = ExecuteReader(query);</para>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(string query)
        {
            DataTable dataTable = new DataTable();

            using (_Connection.SqlConnection)
            {
                _Connection.SqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        dataTable.Load(dataReader);
                    }
                }
                _Connection.SqlConnection.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Example:
        /// <para>string query = "SELECT COUNT(*) FROM TableName";</para>
        /// <para>object count = ExecuteScalar(query);</para>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public object ExecuteScalar(string query)
        {
            object? result = null;

            using (_Connection.SqlConnection)
            {
                _Connection.SqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection))
                {
                    result = sqlCommand.ExecuteScalar();
                }
                _Connection.SqlConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// <para>string query = "INSERT INTO TableName (Username, Email) VALUES ('newuser', 'newuser@example.com')";</para>
        /// <para>int rowsAffected = ExecuteNonQuery(query);</para>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;

            using (_Connection.SqlConnection)
            {
                _Connection.SqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection))
                {
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                _Connection.SqlConnection.Close();
            }
            return rowsAffected;
        }

        /// <summary>
        /// Not tested.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable ExecuteParameterizedReader(string query, string parameterName, object value)
        {
            DataTable dataTable = new DataTable();
            SqlParameter sqlParameter = new SqlParameter(parameterName, value);

            using (_Connection.SqlConnection)
            {
                _Connection.SqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue(parameterName, value);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        dataTable.Load(dataReader);
                    }
                }
                _Connection.SqlConnection.Close();
            }
            return dataTable;
        }
        #endregion
    }
}
