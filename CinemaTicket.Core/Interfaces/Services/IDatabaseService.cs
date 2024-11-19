using CinemaTicket.Core.Dtos;

namespace CinemaTicket.Core.Interfaces.Services
{
    public interface IDatabaseService
    {
        Task<bool> CreateDatabaseAsync();
        Task<DatabaseInfo> GetDatabaseInfo();

        Task<bool> DropDatabaseAsync();
    }
}