namespace App.Core.Interfaces.Services
{
    public interface IDbService
    {
        Task<bool> CreateDatabaseAsync();
    }
}
