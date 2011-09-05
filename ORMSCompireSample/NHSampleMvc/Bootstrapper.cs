using System;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Service.NH3;
using Service.NH3.Repositories;
using Infrastructure.NHibernate;
using Infrastructure;

namespace NHSampleMvc
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["NHSample"];
            
            string providerName = connectionStringSettings.ProviderName;
            string connectionString = connectionStringSettings.ConnectionString;

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.Register(c => new DatabaseFactory(providerName, connectionString)).As<IDatabaseFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PeopleRepository>().As<IPeopleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PeopleService>().As<IPeopleService>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));  

        }
    }
}