using Martian.Domain.AggregateModels.Rover;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.Events
{
    public class RoverMovedDomainEvent : INotification
    {
        public CardinalDirection Direction { get; }

        public string Id { get; }

        public RoverMovedDomainEvent(CardinalDirection direction, string id)
        {
            Direction = direction;
            Id = id;
        }
    }
}
