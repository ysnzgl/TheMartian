using Martian.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Martian.Application.EventHandlers
{
    public class RoverPlacedDomainEventHandler : INotificationHandler<RoverPlacedDomainEvent>
    {
        public async Task Handle(RoverPlacedDomainEvent notification, CancellationToken cancellationToken)
        {
            //Event sourcing için Message broker (Rabbit, Kafka vs.) üzerine publish işlemleri. 
            await Task.CompletedTask;
        }


    }
}
