using LiteDB;
using WinFormsLiteDbFromJson.Entities;
using WinFormsLiteDbFromJson.Filters;

namespace WinFormsLiteDbFromJson.Controllers
{
    public interface IDatabaseService<TEntity> where TEntity : Entity, new()
    {
        LiteCollection<TEntity> GetCollection { get; }

        void Insert(TEntity entity);

        List<TEntity> GetData(Func<TEntity, bool> filterMethod);

        List<TEntity> GetFullData();

        void FullCopyFromOtherDb(IDatabaseService<TEntity> otherDbService);

        public IEnumerable<TEntity> GetAllEntities();

        public List<TEntity> GetFiltredData(FilterOperation<String> filterOperation, string propertyName);
    }
}
