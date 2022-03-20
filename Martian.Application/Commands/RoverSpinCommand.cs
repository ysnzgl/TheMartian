using Martian.Domain.AggregateModels.Rover;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Application.Commands
{
    public class RoverSpinCommand:IRequest
    {
        public Direction Direction { get; }

        public string Id { get; }

        public RoverSpinCommand(Direction direction, string id)
        {
            Direction = direction;
            Id = id;
        }
    }
}
