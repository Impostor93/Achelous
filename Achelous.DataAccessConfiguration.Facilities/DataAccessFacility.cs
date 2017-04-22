using System;
using Achelous.DomainModeling;
using Castle.Core.Configuration;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

namespace Achelous.Facilities.DataAccess
{
    public class DataAccessFacility : IFacility
    {
        public void Init(IKernel kernel, IConfiguration facilityConfig)
        {
            kernel.Register(Castle.MicroKernel.Registration.Component.For<IDataSourceRegister>().ImplementedBy<DataSourceRegister>());
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
