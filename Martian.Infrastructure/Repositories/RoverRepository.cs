using Dapper;
using Martian.Domain.AggregateModels.Rover;
using Martian.Domain.Repositories;
using Martian.Infrastructure.Options;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Martian.Infrastructure.Repositories
{
    public class RoverRepository : IRoverRepository
    {
        private readonly IMediator _mediator;
        private string dbName;
        public RoverRepository(IOptions<SqliteOptions> options, IMediator mediator)
        {
            _mediator = mediator;
            dbName = options.Value.DBName;
        }

        public async Task<Rover> GetAsync(string id)
        {
            using (var conn = new SqliteConnection(dbName))
            {
                var query = @"SELECT r.Id,r.PlateauId,l.X,l.Y,l.Direction FROM Rover r
                              JOIN Location l on l.RoverId=r.Id
                              Where r.Id=@id";

                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();
                var result = await conn.QueryFirstOrDefaultAsync<RoverDto>(query, new { id });
                if (result == null) return null;

                Rover rover = new Rover(result.Id);
                Location location = new Location(result.X, result.Y, result.Direction);
                rover.Place(location, result.PlateauId);
                return rover;
            }
        }

        public async Task SaveAsync(Rover entity)
        {
            await _mediator.DispatchEvents(entity);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
            using (var conn = new SqliteConnection(dbName))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();

                await conn.ExecuteAsync(@"Insert Into Rover(PlateauId,Id)
                                          Values(@PlateauId,@Id)",
                          new { entity.PlateauId, entity.Id });

                await conn.ExecuteAsync(@"Insert Into Location(X,Y,Direction,RoverId)
                                          Values(@X,@Y,@Direction,@Id)",
                         new { entity.Location.X, entity.Location.Y, entity.Location.Direction, entity.Id });
            }
        }

        public async Task UpdateAsync(Rover entity)
        {
            await _mediator.DispatchEvents(entity);
            using (var conn = new SqliteConnection(dbName))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();

                await conn.ExecuteAsync(@"Update Rover  Set PlateauId=@PlateauId
                                          Where Id=@Id",
                                        new { entity.PlateauId, entity.Id });

                await conn.ExecuteAsync(@"Update Location Set X=@X,Y=@Y,Direction=@Direction
                                          Where RoverId=@Id",
                                         new { entity.Location.X, entity.Location.Y, entity.Location.Direction, entity.Id });

            }
        }

        public async Task DeleteAllAsync()
        {
            using (var conn = new SqliteConnection(dbName))
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
                await conn.OpenAsync();
                await conn.ExecuteAsync(@"DELETE FROM Location");
                await conn.ExecuteAsync(@"DELETE FROM Rover");
            }
        }
    }
}
