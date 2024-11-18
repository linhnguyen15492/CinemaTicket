namespace CinemaTicket.Web.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; } = string.Empty; // jwt token
        public string RefreshToken { get; set; } = string.Empty;
    }
}
