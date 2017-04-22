using System;
using System.Reflection;
using Achelous.Facilities.Command;
using Achelous.Facilities.DataAccess;
using Achelous.Facilities.RaoutingConfiguration;
using Achelous.Facilities.RoutingEngine;
using Achelous.DomainModeling;
using Achelous.RaoutingConfiguration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Hosting;

namespace Achelous
{
    class Program
    {
        static IWindsorContainer conteiner;

        static void Main(string[] args)
        {
            conteiner = new WindsorContainer().Install(new ControllerInstaller());
            conteiner.Register(Component.For<ConfigurationBuilder<Configuration>>().Instance(new ConfigurationBuilder<Configuration>("../../RouteConfiguration.xml")));

            var dataAccessConfigBuilder = new ConfigurationBuilder<DataAccessConfiguration>("../../DataAccessConfiguration.xml");
            conteiner.Register(Component.For<ConfigurationBuilder<DataAccessConfiguration>>().Instance(dataAccessConfigBuilder));

            log4net.Config.XmlConfigurator.Configure();

            using (WebApp.Start("http://localhost:9000",
                (config) =>
                {
                    conteiner.AddFacility<CommandFacility>();
                    conteiner.AddFacility<RouteConfigurationFacility>();
                    conteiner.AddFacility<RoutingEngineFacility>();
                    conteiner.AddFacility<DAConfigurationFacility>();
                    conteiner.AddFacility<DataAccessFacility>();

                    conteiner.Resolve<ICommandHandlerRegister>().RegisterHandler(Assembly.Load("Achelous.Commands"));

                    conteiner.Resolve<IDataSourceRegister>().Register(Assembly.Load("Achelous.SqlDataAccess"));

                    var startUp = new Startup();
                    startUp.Configuration(config, conteiner);
                }))
            {
                Console.WriteLine("Server is started if you want to stop it press any button!");
                Console.ReadKey();
            }

            conteiner.Dispose();
        }
    }
}
