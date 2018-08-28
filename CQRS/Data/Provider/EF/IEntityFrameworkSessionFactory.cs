namespace Incoding.Data
{
    using Microsoft.EntityFrameworkCore;
    public interface IEntityFrameworkSessionFactory : ISessionFactory<DbContext>
    {
    }
}