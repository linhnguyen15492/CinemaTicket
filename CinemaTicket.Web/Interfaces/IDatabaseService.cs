using CinemaTicket.Web.Models;

namespace CinemaTicket.Web.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> CreateDatabseAsync();
        Task<bool> DropDatabseAsync();
        Task<DatabaseInfo?> GetDatabaseInfo();
        Task<bool> SeedDataAsync();
    }
}