using System;
using Achelous.DomainModeling;
using log4net;

namespace Achelous.Facilities.Command
{
    public class ErrorHandlingStrategy : IErrorHandlingStrategy
    {
        ILog logger;
        public ErrorHandlingStrategy()
        {
            logger = LogManager.GetLogger(typeof(ErrorHandlingStrategy));
        }

        public void Handle(Exception ex)
        {
            logger.Error(ex);
        }
    }
}
