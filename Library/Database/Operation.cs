using DataVista.Core;
using DataVista.Database;
using DataVista.Database.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVista.Database
{
    public sealed class Operation
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

            try
            {
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
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                _Connection.SqlConnection.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Execute a stored procedure with parameters by passing the name of the procedure and a parameter with a value.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable ExecuteSingleParamProcedure(string procedureName, string parameterName, object value)
        {
            DataTable dataTable = new DataTable();
            SqlParameter sqlParameter = new SqlParameter(parameterName, value);
            try
            {
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
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
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
            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        dataTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
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
            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection);
                    result = sqlCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
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
            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection);
                    rowsAffected = sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
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
            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection);
                    sqlCommand.Parameters.AddWithValue(parameterName, value);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        dataTable.Load(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                _Connection.SqlConnection.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Example usage:
        /// <code>string procedureName = "InsertUser";
        /// bject[] values = { "John", "Doe", 30 };
        /// string[] parameterNames = { "@FirstName", "@LastName", "@Age" };</code>
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="values"></param>
        /// <param name="parameterNames"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public DataTable ExecuteMultiParamProcedure(string procedureName, object[] values, params string[] parameterNames)
        {
            DataTable dataTable = new DataTable();

            if (parameterNames.Length != values.Length)
            {
                throw new ArgumentException("Number of parameter names must match number of values.");
            }
            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();

                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = _Connection.SqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = procedureName;

                    for (int i = 0; i < parameterNames.Length; i++)
                    {
                        sqlCommand.Parameters.AddWithValue(parameterNames[i], values[i]);
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                    dataAdapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
            }
            finally
            {
                _Connection.SqlConnection.Close();
            }
            return dataTable;
        }

        public SqlDataReader ExecuteTransaction(string query)
        {
            SqlDataReader? sqlDataReader = null;
            SqlTransaction? sqlTransaction = null;

            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();
                    sqlTransaction = _Connection.SqlConnection.BeginTransaction();

                    SqlCommand sqlCommand = new SqlCommand(query, _Connection.SqlConnection, sqlTransaction);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    sqlDataReader = sqlCommand.ExecuteReader();

                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
                throw new Exception("Error executing transaction: " + ex.Message);
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }

                _Connection.SqlConnection.Close();
            }
            return sqlDataReader;
        }

        /// <summary>
        /// Same as <see cref="ExecuteTransaction(string)"/> but with optional:
        /// <code>sqlCommand.CommandText = query2;
        ///sqlDataReader = sqlCommand.ExecuteReader();</code>
        /// </summary>
        /// <param name="query1"></param>
        /// <param name="query2"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SqlDataReader ExecuteTransactionReader(string query1, string query2)
        {
            SqlDataReader? sqlDataReader = null;
            SqlTransaction? sqlTransaction = null;

            try
            {
                using (_Connection.SqlConnection)
                {
                    _Connection.SqlConnection.Open();
                    sqlTransaction = _Connection.SqlConnection.BeginTransaction();


                    SqlCommand sqlCommand = new SqlCommand(query1, _Connection.SqlConnection, sqlTransaction);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();

                    sqlCommand.CommandText = query2;
                    sqlDataReader = sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                ExceptionHandler.Log(MethodBase.GetCurrentMethod(), ex);
                throw new Exception("Error executing transaction: " + ex.Message);
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
                _Connection.SqlConnection.Close();
            }
            return sqlDataReader;
        }
        #endregion
    }
}
