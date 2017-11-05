using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webapi.Framework.DAL;
using webapi.Middleware;
using IdentityServer4;
using webapi.config;

namespace webapi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            env.ConfigureNLog("nlog.config");

            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });

            services.AddIdentityServer()
            .AddDeveloperSigningCredential();

            // config
            // uses IOptions<T> for your settings.
            services.AddOptions();
            services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));
            // DI
            services.AddSingleton<IOperationTest>(new OperationTest());
            services.AddSingleton<ICache>(new Cache());

            // // Adds a default in-memory implementation of IDistributedCache.
            // services.AddDistributedMemoryCache();

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "master";
            });

            // services.AddSession(options =>
            // {
            //     // Set a short timeout for easy testing.
            //     options.IdleTimeout = TimeSpan.FromSeconds(10);
            //     options.Cookie.HttpOnly = true;
            // });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env, IApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                });
            }

            // use session
            //app.UseSession();

            // add middleware
            app.UseMiddleware(typeof(MiddlewareTest));
            app.UseMvc();
        }
    }
}
