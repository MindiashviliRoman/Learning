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

        void DeleteColumn(string tName, string colName);

        void DeleteTable(string tName);

        void ClearTable(string tName);

        void SaveDataToDB(DataTable data, string tableName);

        DataTable GetTablesNames();
        DataTable ReadFromTable(string query);
        DataTable ReadFromTableAll(string tableName);
        List<SqlFieldProperty> GetFieldParams(string tableName);
    }
}
