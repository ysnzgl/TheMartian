using Martian.Domain.AggregateModels.Rover;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.Events
{
    public class RoverPlacedDomainEvent : INotification
    {
        public Location location { get; }
        public Guid PlateauId { get; }
        public RoverPlacedDomainEvent(Location location, Guid plateauId)
        {
            this.location = location;
            PlateauId = plateauId;
        }
    }
}
