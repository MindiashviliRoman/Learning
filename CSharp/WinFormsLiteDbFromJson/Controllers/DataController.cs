using WinFormsLiteDbFromJson.Entities;
using WinFormsLiteDbFromJson.DataProviders;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WinFormsLiteDbFromJson.Filters;

namespace WinFormsLiteDbFromJson.Controllers
{
    public class DataController : IDisposable
    {
        private SetupData _setupData;
        private IDataProvider _dataProvider;
        private IDatabaseService<Entity> _dbService;
        private CancellationTokenSource _timerCancellationTokenSource;

        public Action DataUpdated;
        public Action NwDbCreated;

        public DataController(SetupData appSetupData, IDatabaseService<Entity> dbService)
        {
            _setupData = appSetupData;
            _dbService = dbService;
            _setupData.DataUpdateChanged += OnDataUpdateChaged;

            _dataProvider = new RandomApiDataProvider("https://randomuser.me/api/");
        }

        public void Dispose()
        {
            _setupData.DataUpdateChanged -= OnDataUpdateChaged;
        }

        private async void OnDataUpdateChaged(bool enabled)
        {
            if (enabled)
            {
                _timerCancellationTokenSource = new CancellationTokenSource();
                await StartTimerAsync(_timerCancellationTokenSource);
            }
            else
            {
                _timerCancellationTokenSource.Cancel();
            }
        }

        private async Task StartTimerAsync(CancellationTokenSource ct, bool flg = false)
        {
            bool canDisposeToken = true;
            try
            {
                await Task.Delay(_setupData.Delay, ct.Token);
                await UpdateData();
                StartTimerAsync(ct, true);
                canDisposeToken = false;
            }
            catch (OperationCanceledException opCancEx)
            {
                var ss = "";
            }
            finally
            {
                if (canDisposeToken && ct != null)
                    ct.Dispose();
            }
        }



        public async Task UpdateData()
        {
            var answer = await _dataProvider.GetJSONFromRequestAsync();

            //Logging
            //using (var logFileWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", append: true))
            //{
            //    ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            //    {
            //        builder.AddProvider(new LoggerProvider(logFileWriter));
            //    });
          
            //    ILogger<DataController> logger = loggerFactory.CreateLogger<DataController>();

            //    using (logger.BeginScope("[Start Log]"))
            //    {
            //        logger.LogInformation(answer.ToJsonString());
            //    }
            //}

            var users = User.GetUsersFromJson(answer);
            if(_dbService != null)
            {
                foreach(var user in users)
                {
                    _dbService.Insert(user);
                }
                DataUpdated?.Invoke();
            }
            else
            {
                Debug.WriteLine("[DataController]. Not found dbService");
            }
        }

        public List<T> GetData<T>() where T : Entity
        {
            var tst = _dbService.GetAllEntities();

            if (_dbService != null)
            {
                return _dbService.GetFullData().Cast<T>().ToList();
            }
            return null;
        }

        public List<T> GetFiltredData<T>(FilterOperation<String> filterOperation, string propertyName)
        {
            var tst = _dbService.GetAllEntities();

            if (_dbService != null)
            {
                return _dbService.GetFiltredData(filterOperation, propertyName).Cast<T>().ToList();
            }
            return null;
        }

        public void ChangedDbPath(string newPath, bool withCopyData = false, bool destroyOldDb = false)
        {
            var nwDbService = new LiteDBService<Entity>(newPath);
            if (_dbService != null && withCopyData)
            {
                if (withCopyData)
                {
                    nwDbService.FullCopyFromOtherDb(_dbService);
                }
                if (destroyOldDb)
                {

                }
            }
            _dbService = nwDbService;
            NwDbCreated?.Invoke();
        }

        public List<string> GetFieldNames()
        {
            var properties = typeof(User).GetProperties();
            return properties.Select(p => p.Name).ToList();
        }

        public void FillFiltersCombobox(ComboBox cb, string propName)
        {
            if (!String.IsNullOrEmpty(propName)) {
                var prop = typeof(User).GetProperty(propName);

                switch (prop.PropertyType.Name)
                {
                    case nameof(String):
                        cb.DataSource = GetFilters<string>();
                        return;
                    case nameof(Int16):
                        cb.DataSource = GetFilters<Int16>();
                        return;
                    case nameof(Int32):
                        cb.DataSource = GetFilters<Int32>();
                        return;
                    case nameof(Int64):
                        cb.DataSource = GetFilters<Int64>();
                        return;
                    case nameof(UInt16):
                        cb.DataSource = GetFilters<UInt16>();
                        return;
                    case nameof(UInt32):
                        cb.DataSource = GetFilters<UInt32>();
                        return;
                    case nameof(UInt64):
                        cb.DataSource = GetFilters<UInt64>();
                        return;
                    case nameof(Double):
                        cb.DataSource = GetFilters<Double>();
                        return;
                    case nameof(Boolean):
                        cb.DataSource = GetFilters<Boolean>();
                        return;
                }

            }
        }

        public List<FilterOperation<TValue>>GetFilters<TValue>() where TValue : IComparable<TValue>, IConvertible
        {
            var result = new List<FilterOperation<TValue>>();
            result.Add(new FilterOperationEQ<TValue>());
            result.Add(new FilterOperationLE<TValue>());
            result.Add(new FilterOperationGE<TValue>());
            result.Add(new FilterOperationConsists<TValue>());
            return result;
        }

    }
}
