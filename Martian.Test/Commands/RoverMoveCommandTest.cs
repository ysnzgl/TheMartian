using Martian.Application.CommandHandlers;
using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Plateau;
using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Martian.Test.Commands
{
    public class RoverMoveCommandTest
    {
        [Fact]
        public async void RoverMove_Exception()
        {
            var plateauRepository = new Mock<IPlateauRepository>();
            var roverRepository = new Mock<IRoverRepository>();

            var plateauId = Guid.NewGuid().ToString();
            var roverId = Guid.NewGuid().ToString();

            plateauRepository.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(CreatePlateau(plateauId, 2, 2));
            roverRepository.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(PlaceRover(roverId, plateauId));

            var command = new RoverMoveCommand(roverId);
            var sut = new RoverMoveCommandHandler(plateauRepository.Object, roverRepository.Object);


            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.Handle(command, CancellationToken.None));

        }

        async Task<Plateau> CreatePlateau(string plateauId, int width, int height)
        {

            var t = Task.Run(() =>
            {
                var plateau = new Plateau(plateauId);
                plateau.Create(width, height);

                return plateau;
            });

            return await t;

        }

        async Task<Rover> PlaceRover(string roverId, string plateauId)
        {

            var t = Task.Run(() =>
            {
                var rover = new Rover(roverId);
                rover.Place(new Location(1, 2, CardinalDirection.N), plateauId);
                return rover;
            });

            return await t;

        }

    }
}
