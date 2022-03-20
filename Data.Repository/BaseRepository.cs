using System.Linq.Expressions;
using Data.Infra.Context;
using Data.Interfaces;
using MongoDB.Driver;

namespace Data.Repository
{
    public class BaseRepository<TEntity> : IBase<TEntity> where TEntity : class
    {
        protected readonly IMongoContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        public BaseRepository(IMongoContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual Task<TEntity> Create(TEntity entity)
        {
            _context.AddCommand(async () =>
            {
                await DbSet.InsertOneAsync(entity);
            });
            return Task.FromResult(entity);
        }

        public virtual Task<TEntity> Update(TEntity entity, string id)
        {
            _context.AddCommand(async () =>
            {
                await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), entity);
            });

            return Task.FromResult(entity);
        }

        public virtual async Task Delete(string id)
        {
            _context.AddCommand(async () =>
            {
                await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
            });
        }

        public async Task<TEntity> Get(string id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.FirstOrDefault();
        }

        public async Task<TEntity> Get(FilterDefinition<TEntity> filter)
        {
            var data = await DbSet.FindAsync(filter);
            return data.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAll(FilterDefinition<TEntity> filter)
        {
            var all = await DbSet.FindAsync(filter);
            return all.ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}