namespace Incoding.Data
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;

    [UsedImplicitly, ExcludeFromCodeCoverage]
    public class EntityFrameworkUnitOfWorkFactory : IUnitOfWorkFactory
    {
        readonly IEntityFrameworkSessionFactory sessionFactory;
        public EntityFrameworkUnitOfWorkFactory(IEntityFrameworkSessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public IUnitOfWork Create(IsolationLevel level, bool isFlush, string connection = null)
        {
            return new EntityFrameworkUnitOfWork(sessionFactory.Open(connection), level,isFlush);
        }
    }
}