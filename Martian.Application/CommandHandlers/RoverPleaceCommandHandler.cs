using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Plateau;
using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Martian.Application.CommandHandlers
{
    public class RoverPleaceCommandHandler : IRequestHandler<RoverPleaceCommand>
    {
        private readonly IRoverRepository _roverRepository;
        private readonly IPlateauRepository _plateauRepository;

        public RoverPleaceCommandHandler(IRoverRepository roverRepository, IPlateauRepository plateauRepository)
        {
            _roverRepository = roverRepository;
            _plateauRepository = plateauRepository;
        }

        public async Task<Unit> Handle(RoverPleaceCommand request, CancellationToken cancellationToken)
        {
            Rover rover;
            bool update = true;
            rover = await _roverRepository.GetAsync(request.Id);
            if (rover == null)
            {
                update = false;
                rover = new Rover(request.Id);
            }

            Location location = GetLocation(request.Position);
            rover.Place(location, request.PlateauId);

            Plateau plateau = await _plateauRepository.GetAsync(request.PlateauId);
            if (plateau == null)
                throw new ArgumentNullException(nameof(plateau));

            if (rover.Location.X > plateau.Width
             || rover.Location.Y > plateau.Height
             || rover.Location.X < 0
             || rover.Location.Y < 0)
                throw new ArgumentException("Rover is out of bounds");
            if (!update)
                await _roverRepository.SaveAsync(rover);
            else
                await _roverRepository.UpdateAsync(rover);
            return Unit.Value;
        }

        private Location GetLocation(string roverPosition)
        {
            string[] position = roverPosition.Split(" ");
            int x, y;
            if (position.Length == 3 && int.TryParse(position[0].ToString(), out x)
                && int.TryParse(position[1].ToString(), out y)
                && new Regex("^[NEWS]$").Match(position[2].ToString()).Success)
            {
                CardinalDirection cDirection = (CardinalDirection)Enum.Parse(typeof(CardinalDirection), position[2].ToString().ToUpper());
                return new Location(x, y, cDirection);
            }
            else
                throw new ArgumentException("Plateau size input values could not resolved.");
        }
    }
}
