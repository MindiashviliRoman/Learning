
namespace WpfSqlAny.Logic
{
    internal interface IRequestToTables
    {
        void ShowWithUpdateInfo(IDbAdapter dbAdapter, string tableName);
        bool CheckConnectionErrors();
    }
}
