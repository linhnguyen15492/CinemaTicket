﻿using CinemaTicket.Web.Interfaces;
using CinemaTicket.Web.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

string baseUrl = builder.Configuration.GetSection("API_URL").Value!;

//var baseUrl = "http://localhost:5073/api/";

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMovieService, MovieService>(provider => new MovieService(baseUrl));
builder.Services.AddTransient<IShowtimeService, ShowtimeService>(provider => new ShowtimeService(baseUrl));
builder.Services.AddTransient<ITheaterService, TheaterService>(provider => new TheaterService(baseUrl));
builder.Services.AddTransient<IDatabaseService, DatabaseService>(provider => new DatabaseService(baseUrl));
builder.Services.AddTransient<ITicketService, TicketService>(provider =>
{
    return new TicketService(baseUrl, provider.GetRequiredService<UserService>());
});
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient();

builder.Services.AddDistributedMemoryCache();   // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)

builder.Services.AddSession(cfg =>
{                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "22880088";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Sử dụng Session

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
