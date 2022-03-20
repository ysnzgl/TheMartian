using Martian.Application.Commands;
using Martian.Application.Queries;
using Martian.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Martian.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MartianController : ControllerBase
    {
        private readonly ILogger<MartianController> _logger;
        private readonly IMediator _mediator;
        public MartianController(ILogger<MartianController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreatePlateau")]
        public async Task<IActionResult> CreatePlateau([FromBody] PlateauViewModel plateauVM)
        {
            string plateauId = plateauVM.Name;
            var createPlateauCommand = new PlateauCreateCommand(plateauVM.Size, plateauId);
            await _mediator.Send(createPlateauCommand);

            var statusCode = HttpStatusCode.OK;
            string message = $"plateauName = {plateauId}";
            return StatusCode((int)statusCode, message);
        }

        [HttpPost]
        [Route("GoRover")]
        public async Task<IActionResult> GoRover([FromBody] RoverViewModel roverVM)
        {
            string roverId = roverVM.Name;
            var placeRoverCommand = new RoverPleaceCommand(roverVM.RoverPlace, roverVM.PlateauName, roverId);
            await _mediator.Send(placeRoverCommand);

            foreach (var cmd in roverVM.RoverCommands.ToCharArray())
            {
                switch (char.ToUpper(cmd))
                {
                    case 'L':
                        await _mediator.Send(new RoverSpinCommand(Domain.AggregateModels.Rover.Direction.Left, roverId));
                        break;
                    case 'R':
                        await _mediator.Send(new RoverSpinCommand(Domain.AggregateModels.Rover.Direction.Right, roverId));
                        break;
                    case 'M':
                        await _mediator.Send(new RoverMoveCommand(roverId));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(cmd));
                }
            }

            var lastRoverState = await _mediator.Send(new GetRoverQuery { Id = roverId });


            var statusCode = HttpStatusCode.OK;
            string message = $"{roverId} Named Rover: {lastRoverState.Location.X} {lastRoverState.Location.Y} {lastRoverState.Location.Direction}";
            return StatusCode((int)statusCode, message);
        }
    }
}
