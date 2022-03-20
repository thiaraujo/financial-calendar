namespace Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
}