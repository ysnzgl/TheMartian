using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Martian.Domain.Events;

namespace Martian.Application.EventHandlers
{
    public class PlateauCreatedDomainEventHandler : INotificationHandler<PlateauCreatedDomainEvent>
    {
        public async Task Handle(PlateauCreatedDomainEvent notification, CancellationToken cancellationToken)
        {

            //Event sourcing için Message broker (Rabbit, Kafka vs.) üzerine publish işlemleri. 
            await Task.CompletedTask;
        }
    }
}
