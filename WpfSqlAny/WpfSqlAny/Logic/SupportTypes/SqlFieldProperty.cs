using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WpfSqlAny.Logic.SupportTypes
{
    public struct SqlFieldProperty
    {
        public bool IsAutoIncrement;
        public string Name;
        public SqlDataType Type;

        public SqlFieldProperty(bool isKey, string name, SqlDataType type)
        {
            IsAutoIncrement = isKey;
            Name = name;
            Type = type;
        }

        public static DataTable GetDataTable(List<SqlFieldProperty> fields, bool exceptAutoIncrement)
        {
            var dt = new DataTable();
            foreach (var field in fields)
            {
                if(exceptAutoIncrement && !field.IsAutoIncrement)
                {
                    dt.Columns.Add(field.Name);
                }
            }
            return dt;
        }
    }
}
