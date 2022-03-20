using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.Events
{
    public class PlateauCreatedDomainEvent : INotification
    {
        public int Width { get; }
        public int Height { get; }

        public PlateauCreatedDomainEvent(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
