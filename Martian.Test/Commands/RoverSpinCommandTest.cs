using Martian.Application.CommandHandlers;
using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.Repositories;
using MediatR;
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
    public class RoverSpinCommandTest
    {
        [Theory]
        [InlineData(new Object[] { Direction.Left, CardinalDirection.N })]
        [InlineData(new Object[] { Direction.Right, CardinalDirection.N })]
        public async void RoverSpin_Test(Direction direction, CardinalDirection cDirection)
        {

            var roverRepository = new Mock<IRoverRepository>();

            var roverId = Guid.NewGuid().ToString();

            roverRepository.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(PlaceRover(roverId, Guid.NewGuid().ToString(), cDirection));

            CardinalDirection trueDrection = direction == Direction.Left ? CardinalDirection.W : CardinalDirection.E;
            RoverSpinCommand command = new RoverSpinCommand(direction, roverId);
            var commandHandler = new RoverSpinCommandHandler(roverRepository.Object);
            await commandHandler.Handle(command, CancellationToken.None);

            var rover = await roverRepository.Object.GetAsync(roverId);

            Assert.Equal(rover.Location.Direction, trueDrection);


        }

        async Task<Rover> PlaceRover(string roverId, string plateauId, CardinalDirection cDirection)
        {

            var t = Task.Run(() =>
            {
                var rover = new Rover(roverId);
                rover.Place(new Location(1, 2, cDirection), plateauId);
                return rover;
            });

            return await t;

        }
    }
}
