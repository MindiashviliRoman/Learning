using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WpfSqlAny.Logic.SupportTypes
{
    public struct SqlFieldProperty
    {
        public bool IsKey;
        public string Name;
        public SqlDataType Type;

        public SqlFieldProperty(bool isKey, string name, SqlDataType type)
        {
            IsKey = isKey;
            Name = name;
            Type = type;
        }

        public static DataTable GetDataTable(List<SqlFieldProperty> fields)
        {
            var dt = new DataTable();
            foreach (var field in fields)
            {
                dt.Columns.Add(field.Name);
            }
            return dt;
        }
    }
}
