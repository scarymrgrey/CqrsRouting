using Incoding.Maybe;

namespace Incoding.Block.Core
{ 
    using System;

    public abstract class FactoryBase<TInit> where TInit : class, new()
    {
        // ReSharper disable UnassignedField.Global
        #region Fields

        protected readonly TInit init = new TInit();

        #endregion

        // ReSharper restore UnassignedField.Global
        #region Api Methods

        public void Initialize(Action<TInit> initializeAction = null)
        {        
            initializeAction.Do(action => action(this.init));
        }

        #endregion
    }
}