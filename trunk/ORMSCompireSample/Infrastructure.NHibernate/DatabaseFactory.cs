using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection;

using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using ORM.Sample.Common;
using Extensions;
using Domain;
using ISessionFactory = global::NHibernate.ISessionFactory;
using NHibernate.Cfg;

namespace Infrastructure.NHibernate
{
    public class DatabaseFactory:Disposable,IDatabaseFactory
    {
        private static readonly IDictionary<string, IPersistenceConfigurer> persistenceConfigurerMap = BuildPersistenceConfigurerMap();
        private static readonly object sessionFactorySyncLock = new object();

        private readonly string providerName;
        private readonly string connectionString;

        private Database database;

        private static ISessionFactory sessionFactory;

        public DatabaseFactory(string providerName, string connectionString)
        {
          
            this.providerName = providerName;
            this.connectionString = connectionString;
        }

        public Database Get()
        {
            if (database == null)
            {
                EnsureSessionFactory();
                database = new Database(sessionFactory.OpenSession());
            }
            return database;
        }       

        private static IDictionary<string, IPersistenceConfigurer> BuildPersistenceConfigurerMap()
        {
            return new Dictionary<string, IPersistenceConfigurer>(StringComparer.OrdinalIgnoreCase)
                       {
                           { "System.Data.SqlClient", MsSqlConfiguration.MsSql2008 },
                           { "MySql.Data.MySqlClient", MySQLConfiguration.Standard }
                       };
        }

        private void EnsureSessionFactory()
        {
            if (sessionFactory == null)
            {
                lock (sessionFactorySyncLock)
                {
                    if (sessionFactory == null)
                    {
                        IPersistenceConfigurer configurer = persistenceConfigurerMap[providerName];

                        MethodInfo setConnectionString = configurer.GetType()
                                                                 .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod)
                                                                 .First(method => method.Name == "ConnectionString" && method.GetParameters().Any() && method.GetParameters()[0].ParameterType.Equals(typeof(string)));
                        setConnectionString.Invoke(configurer, new[] { connectionString });

                        sessionFactory = Fluently.Configure()   
                                                 .Database(configurer)
                                                 .Mappings(mapping=>mapping.FluentMappings.AddFromAssemblyOf<Database>())
                                                 .ExposeConfiguration(BuildSchema)
                                                 .BuildSessionFactory();
                    }
                }
            }
        }

        private  void BuildSchema(Configuration config)
        {
           
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Execute(false, true, false);                
        }

    }
}
