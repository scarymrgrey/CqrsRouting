using CQRS.Data.Provider.EF;

namespace Incoding.Data
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class BaseDbContext<T> : DbContext
    {
        protected List<Type> mapTypes = MappingCollection<T>.Maps;
        Action<DbContextOptionsBuilder> _optionsBuilderAction;
        public BaseDbContext(Action<DbContextOptionsBuilder> optionsBuilderAction) {
            _optionsBuilderAction = optionsBuilderAction;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var mapping in this.mapTypes)
            {
                modelBuilder.Model.AddEntityType(mapping);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _optionsBuilderAction(optionsBuilder);
        }
    }
  
}