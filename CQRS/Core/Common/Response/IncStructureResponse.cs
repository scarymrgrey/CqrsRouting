namespace Incoding.CQRS
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using JetBrains.Annotations;

    #endregion

    [Obsolete("Please use structure type (int,datetime)")]
    public class IncStructureResponse<T>
    {
        #region Constructors

        [UsedImplicitly, ExcludeFromCodeCoverage]
        public IncStructureResponse() { }

        public IncStructureResponse(T value)
        {
            Value = value;
        }

        #endregion

        #region Properties

        public T Value { get; private set; }

        #endregion
    }
}