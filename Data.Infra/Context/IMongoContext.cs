using MongoDB.Driver;

namespace Data.Infra.Context;

public interface IMongoContext : IDisposable
{
    Task<int> SaveChanges();
    IMongoCollection<T> GetCollection<T>(string name);
    void AddCommand(Func<Task> func);
}