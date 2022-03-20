using Dapper;
using Martian.Domain.AggregateModels.Plateau;
using Martian.Domain.Repositories;
using Martian.Infrastructure.Options;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Martian.Infrastructure.Repositories
{
    public class PlateauRepository : IPlateauRepository
    {
        private readonly IMediator _mediator;
        private string dbName;
        public PlateauRepository(IOptions<SqliteOptions> options, IMediator mediator)
        {
            _mediator = mediator;
            dbName = options.Value.DBName;
        }

        public async Task DeleteAllAsync()
        {
            using (var conn = new SqliteConnection(dbName))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();
                await conn.ExecuteAsync(@"DELETE FROM Plateau");
            }
        }

        public async Task<Plateau> GetAsync(string Id)
        {
            using (var conn = new SqliteConnection(dbName))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();
                var result = await conn.QuerySingleOrDefaultAsync<PlateauDto>("Select * from Plateau Where Id=@Id", new { Id });
                if (result == null) return null;

                Plateau plateau = new Plateau(result.Id);
                plateau.Set(result.Width, result.Height);
                return plateau;
            }
        }

        public async Task SaveAsync(Plateau entity)
        {
            await _mediator.DispatchEvents(entity);
            using (var conn = new SqliteConnection(dbName))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();
                await conn.ExecuteAsync(@"Insert Into Plateau(Width,Height,Id)
                                          Values(@Width,@Height,@Id)",
                                          new { entity.Width, entity.Height, entity.Id });
            }
        }


    }
}
