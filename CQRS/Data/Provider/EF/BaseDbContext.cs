using System.Linq;
using CQRS.Data.Provider.EF;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Incoding.Data
{
    public class BaseDbContext<T> : DbContext
    {
        protected List<Type> mapTypes = MappingCollection<T>.Maps;
        Action<DbContextOptionsBuilder> _optionsBuilderAction;
        public BaseDbContext(Action<DbContextOptionsBuilder> optionsBuilderAction)
        {
            _optionsBuilderAction = optionsBuilderAction;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var mapping in mapTypes)
                if (!modelBuilder.Model.GetEntityTypes().Select(r => r.ClrType).Contains(mapping))
                    modelBuilder.Entity(mapping);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _optionsBuilderAction(optionsBuilder);
        }
    }
}