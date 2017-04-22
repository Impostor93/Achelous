using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achelous.DomainModeling
{
    public interface IErrorHandlingStrategy
    {
        void Handle(Exception ex);
    }
}
