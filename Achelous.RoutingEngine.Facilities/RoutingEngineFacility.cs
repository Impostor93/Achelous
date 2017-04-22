using System;
using Achelous.RoutingEngine;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Achelous.Facilities.RoutingEngine
{
    public class RoutingEngineFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Register(Component.For<IRoutingEngine>().ImplementedBy<Achelous.RoutingEngine.RoutingEngine>());
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
