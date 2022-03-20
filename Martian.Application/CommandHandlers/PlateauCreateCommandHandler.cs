using Martian.Application.Commands;
using Martian.Domain.AggregateModels.Plateau;
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
    public class PlateauCreateCommandHandler : IRequestHandler<PlateauCreateCommand>
    {
        private readonly IPlateauRepository _plateauRepository;

        public PlateauCreateCommandHandler(IPlateauRepository plateauRepository)
        {
            _plateauRepository = plateauRepository;
        }

        public async Task<Unit> Handle(PlateauCreateCommand request, CancellationToken cancellationToken)
        {
            Plateau plateau = new Plateau(request.Id.ToString());
            string[] size = request.Size.Split(" ");
            int width, height;
            if (size.Length == 2 && int.TryParse(size[0].ToString(), out width) && int.TryParse(size[1].ToString(), out height))
            {
                plateau.Create(width, height);
                await _plateauRepository.SaveAsync(plateau);
            }
            else
                throw new ArgumentException("Plateau size input values could not resolved.");


            return Unit.Value;
        }
    }
}
