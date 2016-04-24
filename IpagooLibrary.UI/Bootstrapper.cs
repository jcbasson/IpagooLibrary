using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using AutoMapper;
using Creator.DirectBooking.Api.Service.Utility;
using Creator.DirectBooking.Api.Service.Utility.Interfaces;
using IpagooLibrary.Repository;
using IpagooLibrary.Repository.Infrastructure;
using IpagooLibrary.Repository.Infrastructure.Interfaces;
using IpagooLibrary.Repository.Respositories;
using IpagooLibrary.Service.Services;
using IpagooLibrary.UI.App_Start;
using IpagooLibrary.UI.Hubs;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace IpagooLibrary.UI
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();

            // You can register controllers all at once using assembly scanning...
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().WithParameter("dbConnectionConfig", AppConfig.ConnectionString).InstancePerLifetimeScope();
            builder.RegisterType<AdoNetContext>().As<IAdoNetContext>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BookRepositoryQuery)
                .Assembly).Where(t => t.Name.EndsWith("RepositoryQuery"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BookRepository)
                .Assembly).Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<UrlHttpClient>()
             .As<IUrlHttpClient>()
             .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BookService)
             .Assembly).Where(t => t.Name.EndsWith("Service"))
             .AsImplementedInterfaces().InstancePerLifetimeScope();

            // Register your SignalR hubs.
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            MappingProfile mappingProfile = new MappingProfile();
            var mapper = mappingProfile.GenerateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
        }
    }
}