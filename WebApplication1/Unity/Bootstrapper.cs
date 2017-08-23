using Microsoft.Practices.Unity;
using DAL;
using System.Data.Entity;
using DTO;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using WebApplication1.Unity.MVC.Unity;

namespace WebApplication1.Unity
{
    public class ContainerBootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        /// <summary>
        /// Registering all the types
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<DbContext, Project2Context>(new HierarchicalLifetimeManager());
            // Client
            container.RegisterType<IRepository<Client>, Repository<Client>>();
            // Purchase
            container.RegisterType<IRepository<Purchase>, Repository<Purchase>>();

            MvcUnityContainer.Container = container;
            return container;
        }
    }
}