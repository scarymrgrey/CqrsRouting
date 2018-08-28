namespace Incoding.Data
{
    using Microsoft.EntityFrameworkCore;

    public interface IEFClassMap
    {
        void OnModelCreating(ModelBuilder modelBuilder);
    }
}