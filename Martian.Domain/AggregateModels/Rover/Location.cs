using Martian.Domain.SeedWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.AggregateModels.Rover
{
    public class Location : ValueObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public CardinalDirection Direction { get; private set; }

        public Location(int x, int y, CardinalDirection direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
            yield return Direction;
        }
    }
}
