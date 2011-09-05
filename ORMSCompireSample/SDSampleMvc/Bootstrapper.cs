using System;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Service.SD1;
using Service.SD1.Repositories;
using Infrastructure.SimpleData;
using Infrastructure;

namespace SDSampleMvc
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {            

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => new DatabaseFactory()).As<IDatabaseFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PeopleRepository>().As<IPeopleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PeopleService>().As<IPeopleService>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}