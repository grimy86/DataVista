using DataVista.Database;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace DataVista.Extensions
{
    public static class Methods
    {
        #region OPERATION
        /// <summary>
        /// Executes <see cref="Operation.ExecuteReader(string)"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable ExecuteReader(this DataTable dataTable, string query)
        {
            Operation operation = new Operation();
            return operation.ExecuteReader(query);
        }

        /// <summary>
        /// Executes <see cref="Operation.ExecuteScalar(string)"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this object result, string query)
        {
            Operation operation = new Operation();
            return operation.ExecuteScalar(query);
        }

        /// <summary>
        /// Executes <see cref="Operation.ExecuteNonQuery(string)"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this int result, string query)
        {
            Operation operation = new Operation();
            return operation.ExecuteNonQuery(query);
        }
        #endregion
    }
}
