using Martian.Domain.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Infrastructure.Options
{
    static class MediatorExtension
    {
        public static async Task DispatchEvents(this IMediator mediator, BaseEntity entity)
        {
            if (entity.DomainEvents != null && entity.DomainEvents.Any())
                foreach (var domainEvent in entity.DomainEvents)
                {
                    await mediator.Publish(domainEvent);
                }

            entity.ClearDomainEvents();
        }
    }
}
