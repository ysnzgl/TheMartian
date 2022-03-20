using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Application.Commands
{
    public class RoverPleaceCommand : IRequest
    {
        public string Position { get; }

        public string PlateauId { get; }

        public string Id { get; }

        public RoverPleaceCommand(string position, string plateauId, string id)
        {
            Position = position;
            PlateauId = plateauId;
            Id = id;
        }
    }
}
