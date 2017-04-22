using System;
using System.Collections.Generic;
using System.Threading;
using Achelous.DomainModeling;

namespace Achelous.Facilities.Command
{
    class DefaultCommandRetryStrategy : ICommandRetryStrategy<IResult>
    {
        private int coutOfRetry = 0;
        private int delayNumber = 0;

        private const int maxCountOfRetry = 5;

        public DefaultCommandRetryStrategy()
        {
            delayNumber = 1 * 1000;
        }

        public IResult Execute(Func<IResult> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (coutOfRetry > maxCountOfRetry)
                    throw ex;

                Thread.Sleep(delayNumber);

                coutOfRetry++;
                delayNumber = (1000 * coutOfRetry);

                Execute(func);
            }

            return null;
        }
    }
}
