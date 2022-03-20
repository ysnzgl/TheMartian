using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Application.Commands
{
    public class PlateauCreateCommand : IRequest
    {
        public string Size { get; }

        public string Id { get; }

        public PlateauCreateCommand(string size, string id)
        {
            Size = size;
            Id = id;
        }
    }
}
