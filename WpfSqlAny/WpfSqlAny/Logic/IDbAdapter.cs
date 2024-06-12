using System;
using System.Collections.Generic;
using System.Data;
using WpfSqlAny.Logic.SupportTypes;

namespace WpfSqlAny.Logic
{
    public interface IDbAdapter
    {
        Action<ConnectionStatusType> StatusChanged { get; set; }
        Action<DataTable> DataUpdated { get; set; }

        ConnectionStatusType CurrentStatus { get; set; }

        void Init(string dbName);
        void CreateTable(string name);
        void ConnectToDB();
        void AddColumn(string tableName, string columnName, SqlDataType columnType);

        DataTable GetTablesNames();
        DataTable ReadFromTable(string query);
        DataTable ReadFromTableAll(string tableName);
        List<SqlFieldProperty> GetFieldParams(string tableName);

        void SaveDataToDB(DataTable data, string tableName);
    }
}
