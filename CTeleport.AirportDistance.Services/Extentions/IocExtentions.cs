using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Builder;
using CTeleport.AirportDistance.Services.Contracts;
using CTeleport.AirportDistance.Services.Models;
using CTeleport.AirportDistance.Services.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CTeleport.AirportDistance.Services.Extentions
{
    public static class IocExtensions
    {
        public static void InitializeServices(this ContainerBuilder builder)
        {
            builder
                .RegisterType<AirportService>()
                 .As<IAirportService>()
                 .SingleInstance();

            builder
                .RegisterType<MathService>()
                .As<IMathService>()
                .SingleInstance();

            builder
                .RegisterType<ExternalApiService>()
                 .As<IExternalApiService>()
                 .SingleInstance();

            builder
                .RegisterType<LocationValidator>()
                .As<IValidator<Location>>()
                .SingleInstance();  
            
            builder
                .RegisterType<AirportDistanceValidator<DistanceModel>>()
                .As<IValidator<DistanceModel>>()
                .SingleInstance();

            builder.RegisterLazy();
        }

        public static IRegistrationBuilder<object, ReflectionActivatorData, DynamicRegistrationStyle> RegisterLazy(this ContainerBuilder builder)
        {
            return builder.RegisterGeneric(typeof(Lazier<>))
                .As(typeof(Lazy<>))
                .InstancePerLifetimeScope();
        }
    }

    internal class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IComponentContext context)
            : base(context.Resolve<T>)
        {
        }
    }
}
