using System;

namespace WinFormsLiteDbFromJson
{
    public class SetupData
    {
        public string StartDbPath { get; set; }
        public string DbPath { get; private set; }
        public string DbName { get; private set; }
        public string FullPath { get; private set; }
        public bool EnabledDataUpdate { get; private set; }

        public int Delay {  get; private set; }

        public event Action<string> DbPathChanged;
        public event Action<string> DbFullPathChanged;
        public event Action<bool> DataUpdateChanged;

        public SetupData(string startDbPath)
        {
            StartDbPath = startDbPath;
            DbPath = StartDbPath;
            DbName = "Test1";
            FullPath = DbPath + DbName;        }

        public void OnDbPathChanged(string newDbPath)
        {
            if(DbPath != newDbPath)
            {
                DbPath = newDbPath;
                FullPath = DbPath + DbName;
                DbPathChanged?.Invoke(DbPath);
                DbFullPathChanged?.Invoke(FullPath);
            }
        }

        public void OnDbNameChanged(string newDbName)
        {
            if (DbName != newDbName)
            {
                DbName = newDbName;
                FullPath = DbPath + DbName;
                DbFullPathChanged?.Invoke(FullPath);
            }
        }

        public void OnDelayChanged(int newDelay)
        {
            if(newDelay > 0 && newDelay != Delay)
            {
                Delay = newDelay;
            }
        }

        public void OnDataUpdateChanged(bool enabled)
        {
            if (EnabledDataUpdate != enabled)
            {
                EnabledDataUpdate = enabled;
                DataUpdateChanged?.Invoke(enabled);
            }
        }
    }
}
