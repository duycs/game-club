using System;

namespace Infrastructure.Persistences
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameClubContext _gameClubContext;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(GameClubContext context)
        {
            _gameClubContext = context;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _gameClubContext?.Dispose();

            _disposed = true;
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Commit()
        {
            if (_disposed) throw new ObjectDisposedException(GetType().FullName);
            return _gameClubContext.SaveChanges();
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Rollback()
        {
            Dispose();
        }
    }
}