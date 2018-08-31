namespace Incoding.CQRS
{

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using Incoding.Block.IoC;
    using Incoding.Data;
    using Incoding.Maybe;
    using Microsoft.Extensions.Configuration;


    public class DefaultDispatcher : IDispatcher
    {
        private readonly IConfiguration _configuration;
        private readonly UnitOfWorkCollection unitOfWorkCollection;
        public DefaultDispatcher(IUnitOfWorkFactory _uowFactory,IConfiguration configuration)
        {
            _configuration = configuration;
            unitOfWorkCollection = new UnitOfWorkCollection(_uowFactory);
        }
        


        internal class UnitOfWorkCollection : Dictionary<MessageExecuteSetting, Lazy<IUnitOfWork>>, IDisposable
        {
            private IUnitOfWorkFactory _unitOfWorkFactory;
            public UnitOfWorkCollection(IUnitOfWorkFactory _uowFactory)
            {
                _unitOfWorkFactory = _uowFactory;
            }
       

            public void Dispose()
            {
                //this.Select(r => r.Value)
                //    .DoEach(r =>
                //          {
                //              if (r.IsValueCreated)
                //                  r.Value.Dispose();
                //            });
                Clear();
            }

            public Lazy<IUnitOfWork> AddOrGet(MessageExecuteSetting setting, bool isFlush)
            {
                if (!ContainsKey(setting))
                {
                    Add(setting, new Lazy<IUnitOfWork>(() =>
                                                       {
                                                     
                                                           var isoLevel = setting.IsolationLevel.GetValueOrDefault(isFlush ? IsolationLevel.ReadCommitted : IsolationLevel.ReadUncommitted);
                                                           return _unitOfWorkFactory.Create(isoLevel, isFlush, setting.Connection);
                                                       }, LazyThreadSafetyMode.None));
                }

                return this[setting];
            }


            public void Commit()
            {
                this.Select(r => r.Value)
                    .DoEach(r =>
                            {
                                if (r.IsValueCreated)
                                    r.Value.Commit();
                            });
            }
        }


        public void Push(CommandComposite composite)
        {
            bool isOuterCycle = !unitOfWorkCollection.Any();
            var isFlush = composite.Parts.Any(s => s is CommandBase);
            try
            {
                foreach (var groupMessage in composite.Parts.GroupBy(part => part.Setting, r => r))
                {
                    foreach (var part in groupMessage)
                    {
                        if (isOuterCycle)
                        {
                            if(part.Setting.UID == Guid.Empty)
                            part.Setting.UID = Guid.NewGuid();
                            part.Setting.IsOuter = true;
                        }
                        var unitOfWork = unitOfWorkCollection.AddOrGet(groupMessage.Key, isFlush);
                        part.OnExecute(this, unitOfWork, _configuration);


                        var isFlushInIteration = part is CommandBase;
                        if (unitOfWork.IsValueCreated && isFlushInIteration)
                            unitOfWork.Value.Flush();
                    }
                }
                if (isOuterCycle && isFlush)
                    this.unitOfWorkCollection.Commit();
            }
            finally
            {
                if (isOuterCycle)
                    unitOfWorkCollection.Dispose();
            }
        }

        public TResult Query<TResult>(QueryBase<TResult> message, MessageExecuteSetting executeSetting = null)
        {
            Push(new CommandComposite(message, executeSetting));
            return (TResult)message.Result;
        }
    }
}