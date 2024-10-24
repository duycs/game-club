using System;

namespace Infrastructure.Persistences
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        void Dispose();
        void Rollback();
    }
}