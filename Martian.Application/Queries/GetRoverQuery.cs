using Martian.Domain.AggregateModels.Rover;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Application.Queries
{
    public class GetRoverQuery : IRequest<Rover>
    {
        public string Id { get; set; }
    }

}
