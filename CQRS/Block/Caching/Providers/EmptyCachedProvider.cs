namespace Incoding.Block.Caching
{


    using System.Diagnostics.CodeAnalysis;
    using JetBrains.Annotations;


    ////ncrunch: no coverage start
    [UsedImplicitly, ExcludeFromCodeCoverage]
    internal class EmptyCachedProvider : ICachedProvider
    {
        #region ICachedProvider Members

        public void Delete(ICacheKey key) { }

        public void DeleteAll() { }

        public void Set<T>(ICacheKey key, T instance) where T : class { }

        public T Get<T>(ICacheKey key) where T : class
        {
            return default(T);
        }

        #endregion
    }

    ////ncrunch: no coverage end
}