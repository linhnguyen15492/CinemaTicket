namespace CinemaTicket.Web.Interfaces
{
    public interface IUserService
    {
        bool IsLoggedIn { get; }

        string GetCurrentToken();
        string GetCurrentUser();
        string GetRoles();
        bool IsAuthorized(string role);
        bool IsUserLoggedIn();
        void SetRoles(string roles);
        void SetToken(string token);
        void SetUser(string username);
    }
}