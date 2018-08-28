using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
namespace Incoding.Data
{

    public abstract class EFClassMap<TEntity> : IEFClassMap where TEntity : class
    {
        public virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModel(modelBuilder.Entity<TEntity>());
        }
        public abstract void OnModel(EntityTypeBuilder<TEntity> entity);
    }
}