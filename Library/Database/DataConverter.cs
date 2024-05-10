using DataVista.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataVista.Database
{
    public class DataConverter
    {
        private static string? _winpath = WinPath.MyDocuments + @"\DataConverter";

        /// <summary>
        /// Needs constructor to verify <see cref="Path"/>
        /// </summary>
        DataConverter()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        #region PROPERTIES
        public static string Path
        {
            get
            {
                return _winpath;
            }
            set
            {
                value = _winpath;
            }
        }
        #endregion

        public void SerializeXml(DataSet dataSet)
        {
            dataSet.WriteXmlSchema(Path);
            dataSet.WriteXml(Path);
        }
        public void SerializeXml(DataSet dataSet, string winPath)
        {
            dataSet.WriteXmlSchema(winPath);
            dataSet.WriteXml(winPath);
        }

        public DataSet DeSerializeXml(DataSet dataSet)
        {
            dataSet.ReadXmlSchema(Path);
            dataSet.ReadXml(Path);

            return dataSet;
        }

        public DataSet DeSerializeXml(DataSet dataSet, string winPath)
        {
            dataSet.ReadXmlSchema(winPath);
            dataSet.ReadXml(winPath);

            return dataSet;
        }
    }
}
