using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Martian.Application.CommandHandlers
{
    public class RoverSpinCommandHandler : IRequestHandler<RoverSpinCommand>
    {

        private readonly IRoverRepository _roverRepository;

        public RoverSpinCommandHandler(IRoverRepository roverRepository)
        {
            _roverRepository = roverRepository;
        }

        public async Task<Unit> Handle(RoverSpinCommand request, CancellationToken cancellationToken)
        {
            Rover rover = await _roverRepository.GetAsync(request.Id);
            rover.Spin(request.Direction);
            await _roverRepository.UpdateAsync(rover);
            return Unit.Value;
        }
    }
}
