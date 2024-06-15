using DataVista.Core;
using DataVista.Database;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace DataVista.Core
{
    public static class dvExtensionMethods
    {
        /// <summary>
        /// Sorts Enums by their logical index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        public static string Sort<T>(this Enum @enum) where T : Enum
        {
            StringBuilder builder = new StringBuilder();
            var enumValues = Enum.GetValues(typeof(T));

            for (int i = 0; i < enumValues.Length; i++)
            {
                int index = i; // Index position
                string name = enumValues.GetValue(i).ToString();
                builder.AppendLine($"{index} : {name}");
            }

            return builder.ToString();
        }

        #region SQL OPERATIONS
        /// <summary>
        /// Executes <see cref="dvOperation.ExecuteReader(string)"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable ExecuteReader(this DataTable dataTable, string query)
        {
            dvOperation operation = new dvOperation();
            return operation.ExecuteSQL(query, operation.ExecuteReader);
        }

        /// <summary>
        /// Executes <see cref="dvOperation.ExecuteScalar(string)"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static object ExecuteScalar(this object result, string query)
        {
            dvOperation operation = new dvOperation();
            return operation.ExecuteSQL(query, operation.ExecuteScalar);
        }

        /// <summary>
        /// Executes <see cref="dvOperation.ExecuteNonQuery(string)"/>.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this int result, string query)
        {
            dvOperation operation = new dvOperation();
            return operation.ExecuteSQL(query, operation.ExecuteNonQuery);
        }
        #endregion

        #region EXPERIMENTAL
        //empty section
        #endregion
    }
}
