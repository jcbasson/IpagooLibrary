﻿using Autofac;
using Creator.DirectBooking.Api.Repository.Respositories;
using Ipagoo.ExpressLibary.Repository.Infrastructure;
using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;
using Ipagoo.ExpressLibrary.Api.Controllers;
using Ipagoo.ExpressLibrary.Service.Services;

namespace Ipagoo.ExpressLibrary.Api.Test.Config
{
    public static class AutoFacInitialiser
    {
        public static IContainer AutoFacSetup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BooksController>().As(typeof(BooksController)).SingleInstance();

            builder.RegisterType<AmIHereController>().As(typeof(AmIHereController)).SingleInstance();
            builder.RegisterType<BooksController>().As(typeof(BooksController)).SingleInstance();
          
            builder.RegisterAssemblyTypes(typeof(BookService)
                .Assembly).Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(BookRepository)
                .Assembly).Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().WithParameter("nameOrConnectionString", AppConfig.ConnectionString).SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();

            return builder.Build();
        }
    }
}