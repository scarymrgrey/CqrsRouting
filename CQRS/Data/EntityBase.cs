using System;
using System.ComponentModel.DataAnnotations;

namespace Incoding.Data
{
    #region << Using >>

    using Incoding.Extensions;
    using Incoding.Maybe;
    using MongoDB.Bson.Serialization.Attributes;

    #endregion

    public abstract class EntityBase : IEntity
    {
        public DateTime? DateCreate { get; set; }

        public DateTime? DateModify { get; set; }

        public EntityBase()
        {
            DateCreate = DateTime.Now;
            DateModify = DateTime.Now;
        }
        [Key]
        public virtual int Id { get; set; }

        public override int GetHashCode()
        {
            return Id.ReturnOrDefault(r => r.GetHashCode(), 0);
        }

        public override bool Equals(object obj)
        {
            return this.IsReferenceEquals(obj) && GetHashCode().Equals(obj.GetHashCode());
        }

        public static bool operator ==(EntityBase left, EntityBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityBase left, EntityBase right)
        {
            return !Equals(left, right);
        }
    }
}