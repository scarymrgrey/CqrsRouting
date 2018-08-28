namespace Incoding.Data
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    [ExcludeFromCodeCoverage]
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase<DbContext>
    {
        readonly IDbContextTransaction transaction;
        bool wasCommit;
        public EntityFrameworkUnitOfWork(DbContext session, IsolationLevel level,bool isFlush)
                : base(session)
        {
            //transaction = session.Database.BeginTransaction(level);
            //if (!isFlush)
               // session.Configuration.AutoDetectChangesEnabled = false;
            repository = new EntityFrameworkRepository(session);
        }

        protected override void InternalFlush()
        {
            session.SaveChanges();
        }

        protected override void InternalCommit()
        {
            //transaction.Commit();
            wasCommit = true;
        }

        protected override void InternalSubmit()
        {
           // if (!wasCommit)
               // transaction.Rollback();

           // transaction.Dispose();
        }
    }
}