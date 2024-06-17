using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using WpfSqlAny.Logic.SupportTypes;

namespace WpfSqlAny.Logic
{
    class SqlLiteAdapter : IDbAdapter
    {

        private SQLiteConnection _dbConnection;
        private SQLiteCommand _dbCommand;
        private string _dbPath;

        #region IDbAdapter
        public Action<ConnectionStatusType> StatusChanged { get; set; }
        public Action<DataTable> DataUpdated { get; set; }

        public ConnectionStatusType CurrentStatus { get; set; }

        public void Init(string dbName)
        {
            _dbConnection = new SQLiteConnection();
            _dbCommand = new SQLiteCommand();
            _dbPath = dbName;

            if (!File.Exists(_dbPath))
                SQLiteConnection.CreateFile(_dbPath);
        }

        public void ConnectToDB()
        {
            Connect();
        }

        public void CreateTable(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                App.ErrorMessage("name for creating table is null or empty");
                return;
            }
            try
            {
                _dbConnection = new SQLiteConnection("Data Source=" + _dbPath + ";Version=3;");
                _dbConnection.Open();
                _dbCommand.Connection = _dbConnection;

                _dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + name + " (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT, comment TEXT)";
                _dbCommand.ExecuteNonQuery();

                CurrentStatus = ConnectionStatusType.Connected;
                StatusChanged?.Invoke(ConnectionStatusType.Connected);
            }
            catch (SQLiteException ex)
            {
                CurrentStatus = ConnectionStatusType.Disconnected;
                StatusChanged?.Invoke(ConnectionStatusType.Disconnected);
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public DataTable GetTablesNames()
        {
            DataTable dt = new DataTable();
            try
            {
                _dbConnection = new SQLiteConnection("Data Source=" + _dbPath + ";Version=3;");
                _dbConnection.Open();
                var query = "SELECT name FROM sqlite_master " +
                       "WHERE type = 'table'";// +
                       //" ORDER BY 1";

                using (SQLiteCommand cmd = new SQLiteCommand(query, _dbConnection))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                CurrentStatus = ConnectionStatusType.Disconnected;
                StatusChanged?.Invoke(ConnectionStatusType.Disconnected);
                Console.WriteLine("Error: " + ex.Message);
            }
            return dt;
        }

        public void AddColumn(string tableName, string columnName, SqlDataType columnType)
        {
            try
            {
                /*
                sqlCmd.CommandText = "ALTER TABLE " + tName 
                    + "DROP COLUMN " + colName;
                sqlCmd.ExecuteNonQuery();
                */
                /*
                List<FieldParam> fieldParams = GetFieldParams(dbConn, tName);
                for (int i = 0; i < fieldParams.Count; i++) {
                    if (fieldParams[i].Name == colName) {
                        fieldParams.RemoveAt(i);
                        break;
                    }
                }
                string dublTabName = CreateDublOfTableByFields(tName, fieldParams);
                */

                List<SqlFieldProperty> fieldParams = GetFieldParams(tableName);
                fieldParams.Add(new SqlFieldProperty(false, columnName, columnType));
                string dublTabName = CreateDublOfTableByFields(tableName, fieldParams);

                //Copy info from old Table to new
                string s1 = "INSERT INTO " + dublTabName + "(" + fieldParams[0].Name;
                string s2 = " SELECT " + fieldParams[0].Name;

                for (int i = 1; i < fieldParams.Count - 1; i++)
                {
                    s1 += ", " + fieldParams[i].Name;
                    s2 += ", " + fieldParams[i].Name;
                }
                _dbCommand.CommandText = s1 + ")" + s2 + " FROM " + tableName;
                _dbCommand.ExecuteNonQuery();

                _dbCommand.CommandText = "DROP TABLE " + tableName;
                _dbCommand.ExecuteNonQuery();

                _dbCommand.CommandText = "ALTER TABLE " + dublTabName + " RENAME TO " + tableName;
                _dbCommand.ExecuteNonQuery();

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void DeleteColumn(string tName, string colName)
        {
            try
            {
                List<SqlFieldProperty> fieldParams = GetFieldParams(tName);
                if (fieldParams != null)
                {
                    bool isColNameExist = false;
                    for (int i = 0; i < fieldParams.Count; i++)
                    {
                        if (fieldParams[i].Name == colName)
                        {
                            fieldParams.RemoveAt(i);
                            isColNameExist = true;
                            break;
                        }
                    }
                    if (isColNameExist)
                    {
                        string dublTabName = CreateDublOfTableByFields(tName, fieldParams);

                        //Copy info from old Table to new
                        string s1 = "INSERT INTO " + dublTabName + "(" + fieldParams[0].Name;
                        string s2 = " SELECT " + fieldParams[0].Name;

                        for (int i = 1; i < fieldParams.Count; i++)
                        {// to last element
                            //_dbCommand.CommandText = "INSERT INTO " + dublTabName + "("+ fieldParams[i].Name +")" + " SELECT " + fieldParams[i].Name +" FROM " + tName;
                            s1 += ", " + fieldParams[i].Name;
                            s2 += ", " + fieldParams[i].Name;
                        }
                        _dbCommand.CommandText = s1 + ")" + s2 + " FROM " + tName;
                        _dbCommand.ExecuteNonQuery();

                        _dbCommand.CommandText = "DROP TABLE " + tName;
                        _dbCommand.ExecuteNonQuery();
                        //                _dbCommand.CommandText = "PRAGMA legacy_alter_table=OFF";
                        //                _dbCommand.ExecuteNonQuery();

                        _dbCommand.CommandText = "ALTER TABLE " + dublTabName + " RENAME TO " + tName;
                        _dbCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Table not compaund column with name " + "\"" + colName + "\"");
                    }
                }

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void DeleteTable(string tName)
        {
            try
            {
                _dbCommand.CommandText = "DROP TABLE " + tName;
                _dbCommand.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ClearTable(string tName)
        {
            try
            {
                _dbCommand.CommandText = "DELETE FROM " + tName;
                _dbCommand.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void SaveDataToDB(DataTable data, string tableName)
        {
            try
            {
                int colCnt = data.Columns.Count;

                if (colCnt > 0)
                {
                    _dbCommand.CommandText = "INSERT INTO " + tableName + " (" + "'" + data.Columns[0].ColumnName + "'";
                    for (int i = 1; i < colCnt; i++)
                    {
                        _dbCommand.CommandText += ", '" + data.Columns[i].ColumnName + "'";
                    }
                    _dbCommand.CommandText += ") VALUES";
                    //sqlCmd.CommandText = "INSERT INTO " + tabName + " ('author', 'book') VALUES";
                    if (data.Rows.Count > 0)
                    {
                        _dbCommand.CommandText += "('" + data.Rows[0][0];
                        for (int j = 1; j < colCnt; j++)
                        {
                            _dbCommand.CommandText += "' , '" + data.Rows[0][j];
                        }
                        _dbCommand.CommandText += "')";

                        for (int i = 1; i < data.Rows.Count; i++)
                        {
                            _dbCommand.CommandText += ", ('";
                            for (int j = 1; j < colCnt; j++)
                            {
                                _dbCommand.CommandText += "' , '" + data.Rows[i][j];
                            }
                            _dbCommand.CommandText += "')";
                        }
                        _dbCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public DataTable ReadFromTable(string query)
        {
            DataTable dTable = new DataTable();

            if (_dbConnection.State != ConnectionState.Open)
            {
                App.ErrorMessage("Open connection with database");
                return null;
            }

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, _dbConnection);
                adapter.Fill(dTable);
                DataUpdated?.Invoke(dTable);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dTable;
        }

        public DataTable ReadFromTableAll(string tableName)
        {
            return ReadFromTable($"SELECT * FROM {tableName}");
        }

        public List<SqlFieldProperty> GetFieldParams(string tableName)
        {
            List<SqlFieldProperty> result = new List<SqlFieldProperty>();
            using (SQLiteCommand cmdSQL = _dbConnection.CreateCommand())
            {
                cmdSQL.CommandText = "SELECT * FROM " + tableName;
                SQLiteDataReader dr = cmdSQL.ExecuteReader();

                using (var schemaTable = dr.GetSchemaTable())
                {
                    for (var i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        var row = schemaTable.Rows[i];

                        var columnName = row.Field<string>("ColumnName");
                        var dataTypeName = row.Field<string>("DataTypeName");
                        var isAutoIncreent = row.Field<bool>("IsAutoIncrement");

                        //var numericPrecision = row.Field<int>("NumericPrecision");
                        //var numericScale = row.Field<int>("NumericScale");
                        //var columnSize = row.Field<int>("ColumnSize");
                        //var isKey = row.Field<bool>("IsKey");
                        //var isUnique = row.Field<bool>("IsUnique");
                        //var allowDBNull = row.Field<bool>("AllowDBNull");
                        //var isLong = row.Field<bool>("IsLong");
                        //var isReadOnly = row.Field<bool>("IsReadOnly");
                        //var isRowVersion = row.Field<bool>("IsRowVersion");
                        //var providerType = row.Field<int>("ProviderType");

                        var curParams = new SqlFieldProperty();
                        curParams.Name = columnName;
                        curParams.Type = SqlDataType.GetTypeFromName(dr.GetDataTypeName(i));//dr.GetFieldType(i).ToString();
                        curParams.IsAutoIncrement = isAutoIncreent;

                        result.Add(curParams);
                    }
                }

                dr.Close();
            }

            return result;
        }
        #endregion

        private void Connect()
        {
            if (!File.Exists(_dbPath))
            {
                MessageBox.Show("Please, create DB and blank table (Push \"Create\" button)");
            }

            try
            {
                _dbConnection = new SQLiteConnection("Data Source=" + _dbPath + ";Version=3;");
                _dbConnection.Open();
                _dbCommand.Connection = _dbConnection;

                CurrentStatus = ConnectionStatusType.Connected;
                StatusChanged?.Invoke(ConnectionStatusType.Connected);
            }
            catch (SQLiteException ex)
            {
                CurrentStatus = ConnectionStatusType.Disconnected;
                StatusChanged?.Invoke(ConnectionStatusType.Disconnected);
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ShowFieldsName(string tabName)
        {
            List<string> FieldNames = GetFieldNames2(_dbConnection, tabName);
            string sInfo = "";
            for (int i = 0; i < FieldNames.Count; i++)
            {
                sInfo = sInfo + "\r\n" + FieldNames[i];
            }
            MessageBox.Show(sInfo);
        }

        //public List<string> GetFieldNames(SQLiteConnection conn, string tName)
        //{
        //    string curName = "";
        //    List<string> result = new List<string>();
        //    using (SQLiteCommand cmdSQL = _dbConnection.CreateCommand())
        //    {
        //        cmdSQL.CommandText = "select * from " + tName;
        //        SQLiteDataReader dr = cmdSQL.ExecuteReader();
        //        for (var i = 0; i < dr.FieldCount; i++)
        //        {
        //            curName = dr.GetName(i);
        //            if (curName.ToUpper() != "ID")
        //            {
        //                result.CreateTable(dr.GetName(i));
        //            }
        //        }
        //    }
        //    return result;
        //}

        private List<string> GetFieldNames2(SQLiteConnection conn, string tName)
        {
            List<string> result = new List<string>();
            using (SQLiteCommand cmdSQL = conn.CreateCommand())
            {
                string sqlQuery = @"pragma table_info(" + tName + ");";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                result = App.GetDataByColumnName(App.NAME_TABLE_HEADER, dt);
            }
            return result;
        }

        private string CreateDublOfTableByFields(string tName, List<SqlFieldProperty> paramsOfTable)
        {
            string resultName = "Tmp_" + tName;
            _dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS " + resultName;// + " (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT, comment TEXT)";
            if (paramsOfTable.Count > 0)
            {
                if (paramsOfTable[0].IsAutoIncrement)
                {
                    _dbCommand.CommandText += " (" + paramsOfTable[0].Name + " " + paramsOfTable[0].Type + " PRIMARY KEY AUTOINCREMENT";
                }
                else
                {
                    _dbCommand.CommandText += " (" + paramsOfTable[0].Name + " " + paramsOfTable[0].Type;
                }
                for (int i = 1; i < paramsOfTable.Count; i++)
                {
                    if (paramsOfTable[i].IsAutoIncrement)
                    {
                        _dbCommand.CommandText += ", " + paramsOfTable[i].Name + " " + paramsOfTable[i].Type + " PRIMARY KEY AUTOINCREMENT";
                    }
                    else
                    {
                        _dbCommand.CommandText += ", " + paramsOfTable[i].Name + " " + paramsOfTable[i].Type;
                    }
                }
                _dbCommand.CommandText += ")";
            }
            _dbCommand.ExecuteNonQuery();
            return resultName;
        }
    }
}
