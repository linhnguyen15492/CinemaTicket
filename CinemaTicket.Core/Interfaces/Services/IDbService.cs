namespace CinemaTicket.Core.Interfaces.Services
{
    public interface IDbService
    {
        Task<bool> CreateDatabaseAsync();
    }
}
