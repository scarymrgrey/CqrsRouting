using System.Net;
using CQRS.Block;
using Incoding.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace PogaWebApi.Exceptions
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                        if (contextFeature.Error is CQRSValidationException exc)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            context.Response.ContentType = "application/json";

                            if (!exc.IsJson)
                                await context.Response.WriteAsync(new { error = exc.Message }.ToJsonString());
                            else
                                await context.Response.WriteAsync(exc.Message);
                               
                        }
                        else
                            Log.Logger.Error(contextFeature.Error, contextFeature.Error.Message);
                });
            });
        }
    }
}