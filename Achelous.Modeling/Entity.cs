using System;
using System.Collections.Generic;

namespace Achelous.DomainModeling
{
    public class Entity : IEntity
    {
        public IDictionary<string, object> Attributes { get; private set; }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Entity(Guid id, string name, IDictionary<string, object> attributes)
        {
            Id = id;
            Name = name;
            Attributes = attributes;
        }
    }
}
