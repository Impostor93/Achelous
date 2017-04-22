using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Achelous.Startup))]

namespace Achelous
{
    public class Startup
    {
        public void Configuration(IAppBuilder app, IWindsorContainer container)
        {
            var httpConfig = new HttpConfiguration();
            httpConfig.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "api/{controller}/{version}/{resources}/{id}",
                defaults: new { controller = "RestWebApi", version = 1.0, resources = string.Empty, id = RouteParameter.Optional }
            );

            httpConfig.Formatters.Clear();

            var jsonFormater = new JsonMediaTypeFormatter();
            httpConfig.Formatters.Add(jsonFormater);

            httpConfig.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));

            app.UseWebApi(httpConfig);
        }
    }
}
