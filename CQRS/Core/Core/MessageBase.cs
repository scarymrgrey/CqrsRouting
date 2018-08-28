namespace Incoding.CQRS
{
    #region << Using >>

    using System;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using Incoding.Block.IoC;
    using Incoding.Data;    
    using Incoding.Maybe;
    using Newtonsoft.Json;

    #endregion

    public abstract class MessageBase : IMessage
    {
        #region Fields

        Lazy<IRepository> lazyRepository;
        
        Lazy<MessageDispatcher> messageDispatcher;

        #endregion


        protected IRepository Repository { get { return lazyRepository.Value; } }

        protected MessageDispatcher Dispatcher { get { return messageDispatcher.Value; } }



        public virtual object Result { get; protected set; }


        public virtual MessageExecuteSetting Setting { get; set; }

        public virtual void OnExecute(IDispatcher current, Lazy<IUnitOfWork> unitOfWork)
        {
            Result = null;
            lazyRepository = new Lazy<IRepository>(() => unitOfWork.Value.GetRepository());            
            messageDispatcher = new Lazy<MessageDispatcher>(() => new MessageDispatcher(current, Setting));
            Execute();
        }


        #region Api Methods

        protected abstract void Execute();

        #endregion

        #region Nested classes

        protected class AsyncMessageDispatcher
        {
            #region Fields

            readonly MessageDispatcher dispatcher;

            #endregion

            #region Constructors

            public AsyncMessageDispatcher(MessageDispatcher dispatcher)
            {
                this.dispatcher = dispatcher;
            }

            #endregion

            #region Api Methods

            public Task<TQueryResult> Query<TQueryResult>(QueryBase<TQueryResult> query, Action<MessageExecuteSetting> configuration = null) where TQueryResult : class
            {
                return Task<TQueryResult>.Factory.StartNew(() => dispatcher.Query(query, configuration));
            }

            public Task<object> Push(CommandBase command, Action<MessageExecuteSetting> configuration = null)
            {
                return Task.Factory.StartNew(() =>
                                             {
                                                 dispatcher.Push(command, configuration);
                                                 return command.Result;
                                             });
            }

            #endregion
        }

        protected class MessageDispatcher
        {
            #region Fields

            readonly IDispatcher dispatcher;

            readonly MessageExecuteSetting outerSetting;

            #endregion

            #region Constructors

            public MessageDispatcher(IDispatcher dispatcher, MessageExecuteSetting setting)
            {
                Guard.NotNull("dispatcher", dispatcher, errorMessage: "External dispatcher should not be null on internal dispatcher creation");
                this.dispatcher = dispatcher;
                outerSetting = setting;
            }

            #endregion

            #region Api Methods

            public AsyncMessageDispatcher Async()
            {
                return new AsyncMessageDispatcher(this);
            }

            public IDispatcher New()
            {
                return IoCFactory.Instance.TryResolve<IDispatcher>();
            }

            public TQueryResult Query<TQueryResult>(QueryBase<TQueryResult> query, Action<MessageExecuteSetting> configuration = null)
            {
                configuration.Do(action => action(outerSetting));                
                return dispatcher.Query(query, outerSetting);
            }

            public void Push(CommandBase command, Action<MessageExecuteSetting> configuration = null)
            {
                configuration.Do(action => action(outerSetting));
                dispatcher.Push(command, outerSetting);
            }

            public TResult Push<TResult>(CommandBase command, Action<MessageExecuteSetting> configuration = null)
            {
                configuration.Do(action => action(outerSetting));
                dispatcher.Push(command, outerSetting);
                return (TResult)command.Result;
            }

            #endregion
        }

        #endregion

    }
}