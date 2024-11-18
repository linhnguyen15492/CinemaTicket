using CinemaTicket.Web.Models;
using CinemaTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace CinemaTicket.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _apiUrl = "http://localhost:5073/api/";

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly UserService _userService;

        public AccountController(IHttpClientFactory httpClientFactory, UserService userService)
        {
            _httpClientFactory = httpClientFactory;
            _userService=userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            // Gọi API để đăng nhập và lấy accessToken
            using var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_apiUrl);

            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("username", loginModel.Username),
            //    new KeyValuePair<string, string>("password", loginModel.Password)

            //});
            //var response = await client.PostAsync($"{_apiUrl}/account/login", content);

            var response = await client.PostAsJsonAsync("account/login", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                // Xử lý lỗi đăng nhập
                return BadRequest("Đăng nhập thất bại");
            }

            var accessToken = await response.Content.ReadAsStringAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(accessToken);

            // Lấy danh sách roles từ claim "roles"
            string[] roles = token.Claims.FirstOrDefault(c => c.Type == "roles")?.Value.Split(',')!;

            // Lưu thông tin vào vào session
            HttpContext.Session.SetString("accessToken", accessToken);
            HttpContext.Session.SetString("username", loginModel.Username);

            _userService.SetToken(accessToken);
            _userService.SetUser(loginModel.Username);
            _userService.SetRoles(string.Join(",", roles));

            //return RedirectToAction(nameof(Profile));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            // Lấy accessToken từ session
            var token = HttpContext.Session.GetString("accessToken");

            var username = HttpContext.Session.GetString("username");

            ViewData["username"] = username;

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            // Gọi API khác sử dụng accessToken
            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var response = client.GetAsync("https://your-api-endpoint/profile").Result;


            // Xử lý kết quả trả về từ API
            // ...

            return View();
        }
    }
}