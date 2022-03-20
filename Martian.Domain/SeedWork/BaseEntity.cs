using MediatR;
using System.Collections.Generic;

namespace Martian.Domain.SeedWork
{
    public abstract class BaseEntity
    {
        private string _id;


        public BaseEntity(string id)
        {
            Id = id;
        }

        public virtual string Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        private ICollection<INotification> _domainEvents;

        public ICollection<INotification> DomainEvents => _domainEvents;

        public void AddDomainEvents(INotification notification)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(notification);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
