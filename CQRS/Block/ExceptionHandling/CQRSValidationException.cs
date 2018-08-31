using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Incoding.Extensions;

namespace CQRS.Block
{
    public class CQRSValidationException : ApplicationException
    {
        public virtual bool IsJson => false;

        public CQRSValidationException(string message) : base(message)
        {
        }
    }
    public class CQRSValidationException<T> : CQRSValidationException
    {
        public override bool IsJson => true;
        public CQRSValidationException(Expression<Func<T, object>> prop, string message) : base(BuildMessage(prop, message))
        {
        }

        static string BuildMessage(Expression<Func<T, object>> prop, string message)
        {
            return new Dictionary<string, string[]>()
            {
                { prop.GetMemberName(),new[] { message }}
            }.ToJsonString();
        }
    }
}