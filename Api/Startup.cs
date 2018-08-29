using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CQRS.Block.Http;
using Incoding.CQRS;
using Incoding.Data;
using Incoding.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Incoding.CQRS;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NJsonSchema;
using NSwag.AspNetCore;
using Operations.Entities;
using Operations.Queries;
using PogaWebApi.Exceptions;
using Raven.Abstractions.Extensions;
using Raven.Imports.Newtonsoft.Json;

namespace PogaWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();
            services.AddSingleton(_ => Configuration);
            services.AddScoped<DbContext>(optionsAction => new BaseDbContext<User>(builder => builder.UseSqlServer(Configuration["ConnectionString"])));
            services.AddScoped<IEntityFrameworkSessionFactory, EntityFrameworkSessionFactory>();
            services.AddScoped<IUnitOfWorkFactory, EntityFrameworkUnitOfWorkFactory>();
            services.AddTransient<IDispatcher, DefaultDispatcher>();

            var key = Guid.NewGuid().ToString();
            Configuration["jwt_secret"] = key;
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var dispatcher = context.HttpContext.RequestServices.GetRequiredService<IDispatcher>();
                            var userId = int.Parse(context.Principal.Identity.Name);
                            var user = dispatcher.Query(new GetUserByIdQuery { Id = userId });
                            if (user == null)
                                context.Fail("Unauthorized");

                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.ConfigureExceptionHandler();

            if (!env.IsDevelopment())
                app.UseHsts();

            app.UseAuthentication();
            app.UseHttpsRedirection();

            //            app.UseMvc(routes =>
            //            {
            //                routes.MapRoute(
            //                    name: "default",
            //                    template: "{controller=Home}/{action=Index}/{id?}");
            //            });

            var commands = typeof(User).Assembly
                .GetTypes()
                .Where(r => r.IsImplement(typeof(MessageBase)) && !r.IsInterface && !r.IsAbstract);



            var routeBuilder = new RouteBuilder(app);

            commands.Where(r => r.HasAttribute<HttpVerbAttribute>())
                .ForEach(r =>
            {
                var attr = r.GetCustomAttribute<HttpVerbAttribute>();
                if (attr is GetAttribute)
                    routeBuilder.MapGet(attr.Url, context =>
                    {
                        var queryBase = Activator.CreateInstance(r) as IMessage;
                        var jsonString = context.RequestServices.GetService<IDispatcher>()
                            .Query(queryBase)
                            .ToJsonString();
                        return context.Response.WriteAsync(jsonString);
                    });

                else if (attr is PostAttribute)
                    routeBuilder.MapPost(attr.Url, context =>
                    {
                        var value = Encoding.UTF8.GetString(context.Request.Body.ReadData());
                        var data = JsonConvert.DeserializeObject(value, r);
                        var jsonString = context.RequestServices.GetService<IDispatcher>()
                            .Query(data as IMessage)
                            .ToJsonString();
                        return context.Response.WriteAsync(jsonString);
                    });
            });

            app.UseRouter(routeBuilder.Build());

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
        }

        private async Task Handler(HttpContext context)
        {
            await context.Response.WriteAsync("Hello");
        }
    }
}
