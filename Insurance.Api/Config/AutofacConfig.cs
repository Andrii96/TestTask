using Autofac;
using Autofac.Integration.WebApi;
using Insurance.Domain.Abstract;
using Insurance.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Insurance.Api
{
    public class AutofacConfig
    {
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterInstance(RegisterCustomServices()).As<IContainer>().SingleInstance();

            return builder.Build();
        }
        //Register all database services
        private static IContainer RegisterCustomServices()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<SystemAService>().As<IAction>().SingleInstance();
            builder.RegisterType<SystemBService>().As<IAction>().SingleInstance();
            builder.RegisterType<SystemCService>().As<IAction>().SingleInstance();

            return builder.Build();
        }
    }
}