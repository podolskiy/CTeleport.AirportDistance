using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CTeleport.AirportDistance.Api.Middleware;
using CTeleport.AirportDistance.Api.Models;
using CTeleport.AirportDistance.Services.Contracts;
using CTeleport.AirportDistance.Services.Extentions;
using CTeleport.AirportDistance.Services.Models;
using CTeleport.AirportDistance.Services.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CTeleport.AirportDistance.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
        }

        public IConfigurationRoot Configuration { get; private set; }


        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddControllers()
                .AddFluentValidation();

            services.AddTransient<IValidator<DistanceRequest>, AirportDistanceValidator<DistanceRequest>>();

            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
            });

            services.AddMemoryCache();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            InitConfiguration(builder);

            builder.InitializeServices();
        }

        private void InitConfiguration(ContainerBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(_env.ContentRootPath);

            Configuration = configurationBuilder.Build();

            builder
                .Register<IConfiguration>(provider => Configuration)
                .SingleInstance();


            builder.Register<IAppConfiguration>(provider => provider
                    .Resolve<IConfiguration>()
                    .Get<AppConfiguration>())
                    .SingleInstance();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseResponseCaching();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
