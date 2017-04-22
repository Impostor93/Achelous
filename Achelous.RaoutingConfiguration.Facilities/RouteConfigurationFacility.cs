using System;
using Achelous.DomainModeling;
using Achelous.RaoutingConfiguration;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Achelous.Facilities.RaoutingConfiguration
{
    public class RouteConfigurationFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Register(Component.For<IRouteConfiguration>().UsingFactoryMethod((e) => e.Resolve<ConfigurationBuilder<Configuration>>().Build()).LifestyleSingleton());
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
