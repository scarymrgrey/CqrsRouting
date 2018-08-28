using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Incoding.Data;
using Incoding.Extensions;

namespace CQRS.Data.Provider.EF
{
    public static class MappingCollection<T>
    {
        public static readonly List<Type> Maps;
        static MappingCollection()
        {
            Maps = typeof(T).Assembly.GetTypes()
                .Where(r => r.IsImplement(typeof(EntityBase)) &&
                            !r.IsInterface &&
                            !r.IsAbstract)
                .ToList();
        }
    }
}
