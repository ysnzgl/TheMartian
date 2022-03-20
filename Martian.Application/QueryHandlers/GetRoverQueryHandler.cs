using Martian.Application.Queries;
using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Martian.Application.QueryHandlers
{
    public class GetRoverQueryHandler : IRequestHandler<GetRoverQuery, Rover>
    {
        private readonly IRoverRepository _roverRepository;

        public GetRoverQueryHandler(IRoverRepository roverRepository)
        {
            _roverRepository = roverRepository;
        }

        public async Task<Rover> Handle(GetRoverQuery request, CancellationToken cancellationToken)
        {
            return await _roverRepository.GetAsync(request.Id);

        }
    }
}
