using System;
using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public interface IEntity
    {
        Guid Id { get; }
        string Name { get; }
        IDictionary<string, object> Attributes { get; }
    }
}