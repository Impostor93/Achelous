using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achelous.RaoutingConfiguration
{
    public interface IRouteConfiguration
    {
        List<RouteResource> Resources { get; set; }

        List<DataSourceRoute> Datesources { get; set; }
    }
}
