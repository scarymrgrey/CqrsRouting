namespace Incoding.Data
{
    using Microsoft.EntityFrameworkCore;

    public class EntityFrameworkSessionFactory : IEntityFrameworkSessionFactory
    {
        readonly DbContext createDbContext;
        public EntityFrameworkSessionFactory(DbContext create)
        {
            this.createDbContext = create;
        }
        public DbContext Open(string connectionString)
        {
            return createDbContext;
        }
    }
}