using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WpfSqlAny.Logic.SupportTypes
{
    public class SqlFieldProperty
    {
        public bool IsAutoIncrement;
        public bool IsKey;
        public string Name;
        public SqlDataType Type;

        public SqlFieldProperty(bool isKey, bool isAutoIncrement, string name, SqlDataType type)
        {
            IsKey = isKey;
            IsAutoIncrement = isAutoIncrement;
            Name = name;
            Type = type;
        }

        public static DataTable GetDataTable(List<SqlFieldProperty> fields, bool exceptAutoIncrement)
        {
            var dt = new DataTable();
            var i = 0;
            foreach (var field in fields)
            {
                if(exceptAutoIncrement && !field.IsAutoIncrement)
                {
                    dt.Columns.Add(field.Name);
                    dt.Columns[i].DataType = field.Type.GetMappedType();
                    i++;
                }
            }
            return dt;
        }
    }
}
