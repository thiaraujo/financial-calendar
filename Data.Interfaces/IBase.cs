using MongoDB.Driver;

namespace Data.Interfaces
{
    public interface IBase<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity, string id);
        Task Delete(string id);
        Task<TEntity> Get(string id);
        Task<TEntity> Get(FilterDefinition<TEntity> filter);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filtern);
    }
}