using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.AggregateModels.Plateau
{
    public class PlateauDto
    {
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
