using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.AggregateModels.Rover
{
    public enum CardinalDirection
    {
        [Description("North - Kuzey")]
        N = 0,
        [Description("West - Batı")]
        W = 1,
        [Description("South - Güney")]
        S = 2,
        [Description("East - Doğu")]
        E = 3
    }

    public enum Direction
    {
        Left, Right
    }
}
