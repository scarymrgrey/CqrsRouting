namespace Incoding.Data
{
    using System;
    public abstract class UnitOfWorkBase<TSession> : IUnitOfWork
            where TSession : class, IDisposable
    {
        protected UnitOfWorkBase(TSession session)
        {
            this.session = session;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                InternalSubmit();
                session.Dispose();
            }

            disposed = true;
        }

        protected abstract void InternalSubmit();

        protected abstract void InternalFlush();

        protected abstract void InternalCommit();

        #region Fields

        protected readonly TSession session;

        protected IRepository repository;

        bool disposed;

        #endregion

        public IRepository GetRepository()
        {
            return repository;
        }

        public void Commit()
        {
            InternalCommit();
        }

        public void Flush()
        {
            if (!disposed)
                InternalFlush();
        }
    }
}