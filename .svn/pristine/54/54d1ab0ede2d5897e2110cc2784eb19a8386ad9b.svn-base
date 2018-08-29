namespace Incoding.CQRS
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Incoding.Data;
    using JetBrains.Annotations;

    #endregion

    [UsedImplicitly, ExcludeFromCodeCoverage]
    public class EntityByIdSpec<TEntity> : Specification<TEntity> where TEntity : IEntity
    {


        readonly int id;

        public EntityByIdSpec(int id)
        {
            this.id = id;
        }

        public override Expression<Func<TEntity, bool>> IsSatisfiedBy()
        {
            return r => r.Id == this.id;
        }
    }
}