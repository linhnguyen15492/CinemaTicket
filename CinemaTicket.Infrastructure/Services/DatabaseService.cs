using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
namespace CinemaTicket.Infrastructure.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly CinemaTicketContext _context;

        public DatabaseService(CinemaTicketContext context)
        {
            _context = context;
        }

        public async Task<DatabaseInfo> GetDatabaseInfo()
        {
            var db = new DatabaseInfo()
            {
                CanConnect = await _context.Database.CanConnectAsync(),
                DatabaseName = _context.Database.GetDbConnection().Database,
                TableNames = new List<string>(),
            };

            var tableNames = _context.Model.GetEntityTypes()
                .Select(t => t.GetTableName())
                .Distinct()
                .ToArray();

            if (tableNames.Length == 0)
            {
                return db;
            }
            else
            {
                db.TableNames.AddRange(tableNames!);
            }

            if (db.CanConnect)
            {
                db.IsSeeded = await _context.Movies.AnyAsync();
            }

            return db;
        }

        public async Task<bool> CreateDatabaseAsync()
        {
            var res = await _context.Database.EnsureCreatedAsync();

            return res;
        }

        public async Task<bool> DropDatabaseAsync()
        {
            var res = await _context.Database.EnsureDeletedAsync();

            return res;
        }
    }
}