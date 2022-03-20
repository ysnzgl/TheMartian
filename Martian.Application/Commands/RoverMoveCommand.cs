using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Application.Commands
{
    public class RoverMoveCommand: IRequest
    {
        public string Id { get; }

        public RoverMoveCommand(string id)
        {
            Id = id;
        }
    }
}
