using System.Data;

namespace WpfSqlAny.Logic
{
    internal interface IDatabaseProvider
    {
        DataTable FindDatabases();
        void CreateNewDb();
    }
}
