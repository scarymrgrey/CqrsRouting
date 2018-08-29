namespace Incoding.Extensions
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Routing;

    public static class AnonymousHelper
    {
 
        public static RouteValueDictionary ToDictionary(object anonymous)
        {
            if (anonymous == null)
                return new RouteValueDictionary();

            if (anonymous.GetType().IsAnyEquals(typeof(Dictionary<string, object>), typeof(IDictionary<string, object>)))
                return new RouteValueDictionary(anonymous as Dictionary<string, object> ?? new Dictionary<string, object>());

            return anonymous as RouteValueDictionary ?? new RouteValueDictionary(anonymous);
        }
    }
}