using System.Reflection;
using System.Web.Http.Controllers;
using Achelous.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Achelous
{
    internal class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assembly = Assembly.GetAssembly(typeof(RestWebApiController));
            container.Register(Classes.FromAssembly(assembly).BasedOn(typeof(IHttpController)).LifestyleTransient());
        }
    }
}