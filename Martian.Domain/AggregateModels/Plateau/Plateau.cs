using Martian.Domain.Events;
using Martian.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.AggregateModels.Plateau
{
    public class Plateau : BaseEntity, IAggregateRoot
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Plateau(string id) : base(id)
        {
        }

        public void Set(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public void Create(int width, int height)
        {
            Set(width, height);
            AddDomainEvents(new PlateauCreatedDomainEvent(width, height));
        }
    }
}
