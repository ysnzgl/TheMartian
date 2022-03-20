using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Plateau;
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
    public class RoverMoveCommandHandler : IRequestHandler<RoverMoveCommand>
    {
        private readonly IPlateauRepository _plateauRepository;
        private readonly IRoverRepository _roverRepository;

        public RoverMoveCommandHandler(IPlateauRepository plateauRepository, IRoverRepository roverRepository)
        {
            _plateauRepository = plateauRepository;
            _roverRepository = roverRepository;
        }

        public async Task<Unit> Handle(RoverMoveCommand request, CancellationToken cancellationToken)
        {
            Rover rover = await _roverRepository.GetAsync(request.Id);
            if(rover == null)
                throw new ArgumentNullException(nameof(rover));
            rover.Move();

            Plateau plateau = await _plateauRepository.GetAsync(rover.PlateauId);

            if (rover.Location.X > plateau.Width
                || rover.Location.Y > plateau.Height
                || rover.Location.X < 0
                || rover.Location.Y < 0)
                throw new ArgumentException("Rover is out of bounds");

            await _roverRepository.UpdateAsync(rover);
            return Unit.Value;


        }
    }
}
