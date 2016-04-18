using Microsoft.Practices.Unity;
using System.Web.Http;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Service.Services;
using Unity.WebApi;

namespace Romain_Gauthier.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<INewsTypeService, NewsTypeService>();
            container.RegisterType<INewsService, NewsService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}