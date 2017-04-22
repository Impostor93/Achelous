using System;

namespace Achelous.DomainModeling
{
    public interface ICommandRetryStrategy<T>
    {
        T Execute(Func<T> func);
    }
}
