using CinemaTicket.Web.Models;
using Newtonsoft.Json;

namespace CinemaTicket.Web.Services
{
    public class CartService
    {
        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpContext _httpContext;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpContext = _httpContextAccessor.HttpContext!;
        }


        // Lấy cart từ Session (danh sách CartItem)
        public Ticket? GetCart()
        {
            var session = _httpContext.Session;
            string jsoncart = session.GetString(CARTKEY)!;

            if (jsoncart is not null)
            {
                return JsonConvert.DeserializeObject<Ticket>(jsoncart);
            }

            return null;
        }

        // Xóa cart khỏi session
        public void ClearCart()
        {
            _httpContext.Session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        public void SaveCartSession(Ticket ticket)
        {
            var session = _httpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ticket);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
