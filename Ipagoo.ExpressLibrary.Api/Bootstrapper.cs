using Autofac;
using Autofac.Integration.WebApi;
using Creator.DirectBooking.Api.Repository.Respositories;
using Ipagoo.ExpressLibary.Repository.Infrastructure;
using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;
using Ipagoo.ExpressLibrary.Service.Services;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Ipagoo.ExpressLibrary.Api
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            var configuration = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(configuration);

            builder.RegisterAssemblyTypes(typeof(BookService)
         .Assembly).Where(t => t.Name.EndsWith("Service"))
         .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().WithParameter("dbConnectionConfig", AppConfig.ConnectionString).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BookRepository)
                .Assembly).Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}