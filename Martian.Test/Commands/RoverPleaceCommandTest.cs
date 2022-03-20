using Martian.Application.CommandHandlers;
using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Plateau;
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
    public class RoverPleaceCommandTest
    {

        [Theory]
        [InlineData(new object[] { "1 5 5" })]
        [InlineData(new object[] { "1 H 2" })]
        [InlineData(new object[] { "8 A" })]
        [InlineData(new object[] { "A A" })]
        [InlineData(new object[] { "8 7 K" })]
        [InlineData(new object[] { "1 6 N" })]
        public async void Handle_Invalid(string input)
        {

            var plateauRepository = new Mock<IPlateauRepository>();
            var roverRepository = new Mock<IRoverRepository>();

            plateauRepository.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(CreatePlateau(5, 5));

            var plateauId = Guid.NewGuid().ToString().ToString();
            var roverId = Guid.NewGuid().ToString().ToString();
            var command = new RoverPleaceCommand(input, plateauId, roverId);
            var sut = new RoverPleaceCommandHandler(roverRepository.Object, plateauRepository.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.Handle(command, CancellationToken.None));


        }



        [Theory]
        [InlineData(new object[] { "1 2 N" })]
        [InlineData(new object[] { "4 3 W" })]

        public async void Handle_Valid(string input)
        {

            var plateauRepository = new Mock<IPlateauRepository>();
            var roverRepository = new Mock<IRoverRepository>();

            plateauRepository.Setup(x => x.GetAsync(It.IsAny<string>())).Returns(CreatePlateau(5, 5));

            var plateauId = Guid.NewGuid().ToString();
            var roverId = Guid.NewGuid().ToString();
            var command = new RoverPleaceCommand(input, plateauId, roverId);
            var sut = new RoverPleaceCommandHandler(roverRepository.Object, plateauRepository.Object);
            var result = await sut.Handle(command, CancellationToken.None);


            Assert.True(Unit.Value == result);

        }


        async Task<Plateau> CreatePlateau(int width, int height)
        {

            var t = Task.Run(() =>
            {
                var plateau = new Plateau(Guid.NewGuid().ToString().ToString());
                plateau.Create(width, height);

                return plateau;
            });

            return await t;

        }


    }
}
