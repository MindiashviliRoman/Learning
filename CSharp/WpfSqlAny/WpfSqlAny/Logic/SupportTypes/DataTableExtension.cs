using System.Data;

namespace WpfSqlAny.Logic.SupportTypes.Extension
{
    internal static class DataTableExtension
    {
        public static DataTable GetTransposedTable(this DataTable table)
        {
            var nwTable = new DataTable();
            for(var i = 0; i < table.Rows.Count; i++)
            {
                nwTable.Columns.Add(i.ToString());
            }

            for(var i = 0; i < table.Columns.Count; i++)
            {
                var r = nwTable.NewRow();
                for(var j = 0; j < table.Rows.Count; j++)
                {
                    r[j] = table.Rows[j][i];
                }
                nwTable.Rows.Add(r);
            }

            return nwTable;
        }
    }
}
