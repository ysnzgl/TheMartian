using Martian.Application.CommandHandlers;
using Martian.Application.Commands;
using Martian.Domain.Repositories;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Martian.Test.Commands
{
    public class PlateauCreateCommandTest
    {

        [Theory]
        [InlineData(new object[] { "5 5" })]
        [InlineData(new object[] { "50 25" })]
        [InlineData(new object[] { "50 100" })]

        public async void Handle_Valid(string command)
        {
            var plateauRepository = new Mock<IPlateauRepository>();
            var plateauId = Guid.NewGuid().ToString();
            var commandModel = new PlateauCreateCommand(command, plateauId);
            var commandHandler = new PlateauCreateCommandHandler(plateauRepository.Object);

            var result = await commandHandler.Handle(commandModel, CancellationToken.None);
            Assert.True(Unit.Value == result);
        }

        [Theory]
        [InlineData(new object[] { "1 2 4" })]
        [InlineData(new object[] { "50 a" })]
        [InlineData(new object[] { "a A" })]
        public async void Handle_InValid(string command)
        {
            var plateauRepository = new Mock<IPlateauRepository>();
            var plateauId = Guid.NewGuid().ToString();
            var commandModel = new PlateauCreateCommand(command, plateauId);
            var commandHandler = new PlateauCreateCommandHandler(plateauRepository.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await commandHandler.Handle(commandModel, CancellationToken.None));
        }
    }
}
