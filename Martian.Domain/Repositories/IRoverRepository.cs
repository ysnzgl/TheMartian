using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian.Domain.Repositories
{
    public interface IRoverRepository : IRepository<Rover>
    {      
        Task UpdateAsync(Rover entity);

    }
}
