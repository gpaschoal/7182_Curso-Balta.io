using Flunt.Notifications;
using System;

namespace Store.Domain.Entities
{
    public abstract class Entity : Notifiable, IEquatable<Entity>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}