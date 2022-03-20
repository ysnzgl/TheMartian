using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.AggregateModels.Rover
{
    public class RoverDto
    {
        public string Id { get; set; }
        public string PlateauId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public CardinalDirection Direction { get; set; }

    }
}
