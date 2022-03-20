using Martian.Domain.Events;
using Martian.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.AggregateModels.Rover
{
    public class Rover : BaseEntity, IAggregateRoot
    {
        public Location Location { get; private set; }

        public string PlateauId { get; private set; }
        public Rover(string id) : base(id)
        {
        }

        public void Place(Location location, string plateauId)
        {
            Location = location;
            PlateauId = plateauId;
        }

        public void Spin(Direction direction)
        {
            CardinalDirection cardinalDirection = GetDirection(direction);
            Location = new Location(this.Location.X, this.Location.Y, cardinalDirection);
            AddDomainEvents(new RoverMovedDomainEvent(Location.Direction, Id));
        }

        public void Move()
        {
            int x = Location.X;
            int y = Location.Y;
            switch (Location.Direction)
            {
                case CardinalDirection.N:
                    y += 1;
                    break;
                case CardinalDirection.W:
                    x -= 1;
                    break;
                case CardinalDirection.S:
                    y -= 1;
                    break;
                case CardinalDirection.E:
                    x += 1;
                    break;
                default:
                    throw new ArgumentException("Invalid Direction!!!");
            }
            Location = new Location(x, y, Location.Direction);
            AddDomainEvents(new RoverMovedDomainEvent(Location.Direction, Id));

        }

        private CardinalDirection GetDirection(Direction direction)
        {
            CardinalDirection cDirection = this.Location.Direction;
            switch (this.Location.Direction)
            {
                case CardinalDirection.N:
                    cDirection = direction == Direction.Left
                                            ? CardinalDirection.W
                                            : CardinalDirection.E;
                    break;
                case CardinalDirection.W:
                    cDirection = direction == Direction.Left
                                            ? CardinalDirection.S
                                            : CardinalDirection.N;
                    break;
                case CardinalDirection.S:
                    cDirection = direction == Direction.Left
                                           ? CardinalDirection.E
                                           : CardinalDirection.W;
                    break;
                case CardinalDirection.E:
                    cDirection = direction == Direction.Left
                                           ? CardinalDirection.N
                                           : CardinalDirection.S;
                    break;
                default:
                    throw new ArgumentException("Invalid Direction!!!");
            }
            return cDirection;
        }
    }
}
