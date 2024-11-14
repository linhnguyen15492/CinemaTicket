namespace App.Core.Interfaces.Services
{
    public interface ISeedDataService
    {
        Task SeedDataAsync();

        Queue<string> Messages { get; set; }
    }
}
