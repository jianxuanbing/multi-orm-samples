using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using Infrastructure.EF4;
using Service.EF4;
using Service.EF4.Repositories;
using Infrastructure;

namespace EFSampleMvc
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // e.g. container.RegisterType<ITestService, TestService>();            
            container.RegisterType<IDatabaseFactory, DatabaseFactory>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IPeopleRepository, PeopleRepository>();
            container.RegisterType<IPeopleService, PeopleService>();
            container.RegisterControllers();

            return container;
        }
    }
}