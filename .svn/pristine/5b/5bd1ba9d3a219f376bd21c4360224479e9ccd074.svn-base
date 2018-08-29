namespace Incoding.CQRS
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Block;
    using Incoding.Data;
    using JetBrains.Annotations;

    #endregion

    public class GetEntityByIdQuery<T> : QueryBase<T> where T : class, IEntity, new()
    {
        #region Constructors

        [UsedImplicitly, ExcludeFromCodeCoverage]
        public GetEntityByIdQuery() { }

        ////ncrunch: no coverage start
        public GetEntityByIdQuery(int id)
        {
            Id = id;
        }

        #endregion

        ////ncrunch: no coverage end
        #region Properties

        public int Id { get; set; }

        #endregion

        #region Override

        protected override T ExecuteResult()
        {
            return Repository.GetById<T>(Id);
        }

        #endregion
    }
}