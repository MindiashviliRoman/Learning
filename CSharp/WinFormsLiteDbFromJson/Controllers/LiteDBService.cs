using LiteDB;
using WinFormsLiteDbFromJson.Entities;
using WinFormsLiteDbFromJson.Filters;

namespace WinFormsLiteDbFromJson.Controllers
{
    public class LiteDBService<TEntity> : IDatabaseService<TEntity> where TEntity : Entity, new()
    {
        private string _stringConnection;
        private LiteDatabase _db;
        protected LiteCollection<TEntity> _collection;
        public LiteDBService(string filePath) //"filename={0};journal=false" GetLocalFilePath("LiteDB.db")
        {
            _stringConnection = string.Format("filename={0};journal=false", filePath);
            _db = new LiteDatabase(_stringConnection);
            _collection = (LiteCollection<TEntity>)_db.GetCollection<TEntity>();
        }

        #region Interface IDatabaseService

        LiteCollection<TEntity> IDatabaseService<TEntity>.GetCollection => _collection;

        public void Insert(TEntity entity)
        {
            _collection.Insert(entity);
        }

        public List<TEntity> GetFullData()
        {
            if(_collection != null)
            {
                var count = _collection.Count();
                var result = new List<TEntity>(count);
                foreach(var item in _collection.FindAll())
                {
                    result.Add(item);
                }
                return result;
            }
            return null;
        }

        public List<TEntity> GetData(Func<TEntity, bool> filterMethod)
        {
            if (_collection != null)
            {
                var result = new List<TEntity>();
                foreach (var item in _collection.FindAll())
                {
                    if(filterMethod(item))
                        result.Add(item);
                }
                return result;
            }
            return null;
        }

        public List<TEntity> GetFiltredData(FilterOperation<String> filterOperation, string propertyName)
        {
            if (_collection != null)
            {
                var result = new List<TEntity>();
                foreach (var item in _collection.FindAll())
                {
                    var property = item.GetType().GetProperty(propertyName);
                    var value = property.GetValue(item, null);
                    if (value != null && filterOperation.CheckFilter(value.ToString()))
                        result.Add(item);
                }
                return result;
            }
            return null;
        }

        public void FullCopyFromOtherDb(IDatabaseService<TEntity> otherDbService)
        {
            var otherCollection = otherDbService.GetCollection;
            if (otherCollection != null)
            {
                var count = _collection.Count();
                foreach (var item in _collection.FindAll())
                {
                    Insert(item);
                }
            }
        }

        public IEnumerable<TEntity> GetAllEntities()
        {
            var names = _db.GetCollectionNames();
            List<TEntity> result = new List<TEntity>();
            foreach (var name in names)
            {
                var doc = _db.GetCollection(name, BsonAutoId.Int32);
                foreach (var item in _collection.FindAll())
                {
                    result.Add(item);
                }
            }
            return result;
        }
        #endregion
    }
}
