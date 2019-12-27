using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolCopter.Api.Services;
using CoolCopter.Registration.Core.Copter;
using CoolCopter.Registration.Data;
using CoolCopter.Registration.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoolCopter.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddGrpc();
            services.AddControllers();
            services.Configure<CoolCopterConfiguration>(Configuration.GetSection("CoolCopter"));
            services.AddTransient<ICopterService, CopterService>();
            services.AddTransient<ICopterRepository, CopterRepository>();
            services.AddTransient(provider =>
            {
                var dbConnectionString = provider.GetService<IOptions<CoolCopterConfiguration>>().Value.RegistrationDbConnectionString;
                var option = new DbContextOptionsBuilder<RegistrationContext>()
                        .UseSqlServer(dbConnectionString,
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null);
                        })
                .Options;
                return new RegistrationContext(option);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogDebug($"In Development mode");
                app.UseDeveloperExceptionPage();
            }

            logger.LogDebug($"Setting up CORS");
            app.UseCors();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<EdgeService>();
                endpoints.MapGrpcService<RegistrationService>();
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    logger.LogDebug($"Communication with gRPC endpoints must be made through a gRPC client.");
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
