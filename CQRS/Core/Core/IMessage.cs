﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace Incoding.CQRS
{
    #region << Using >>

    using System;
    using Incoding.Data;
    using Newtonsoft.Json;

    #endregion

    public interface IMessage 
    {
        [BindNever]
        object Result { get; }
        [BindNever]
        MessageExecuteSetting Setting { get; set; }

        void OnExecute(IDispatcher current, Lazy<IUnitOfWork> unitOfWork, IConfiguration configuration);
    }
}