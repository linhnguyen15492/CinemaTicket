using CinemaTicket.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicket.Web.Services
{
    public class DatabaseService : BaseService
    {
        public DatabaseService(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        [HttpPost]
        public async Task<bool> CreateDatabseAsync()
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                using HttpResponseMessage response = await client.PostAsync("home/db/create-database", null);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> DropDatabseAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            using HttpResponseMessage response = await client.PostAsync("home/db/drop-database", null);

            if (response.IsSuccessStatusCode)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SeedDataAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            using HttpResponseMessage response = await client.PostAsync("home/db/seed", null);

            if (response.IsSuccessStatusCode)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<DatabaseInfo?> GetDatabaseInfo()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            try
            {
                using HttpResponseMessage response = await client.GetAsync("home/db/info");

                if (response.IsSuccessStatusCode)
                {
                    var info = await response.Content.ReadFromJsonAsync<DatabaseInfo>();

                    return info;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
