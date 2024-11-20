namespace CinemaTicket.Web.Services
{
    // Tạo một lớp service để quản lý thông tin người dùng
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public bool IsLoggedIn => !string.IsNullOrEmpty(GetCurrentToken());

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("Token");
        }

        public string GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("User");
        }

        public bool IsUserLoggedIn()
        {
            var context = _httpContextAccessor.HttpContext;
            return context.User.Identities.Any(x => x.IsAuthenticated);
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Token", token);

        }

        public void SetUser(string username)
        {
            _httpContextAccessor.HttpContext.Session.SetString("User", username);

        }

        public void SetRoles(string roles)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Roles", roles);
        }

        public string GetRoles()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("Roles");
        }

        public bool IsAuthorized(string role)
        {
            return GetRoles().Contains(role);
        }
    }
}
