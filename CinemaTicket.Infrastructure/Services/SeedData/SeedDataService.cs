using Microsoft.EntityFrameworkCore;
using CinemaTicket.Core.Interfaces.Services;
using CinemaTicket.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Enums;

namespace CinemaTicket.Infrastructure.Services.SeedData
{
    public class SeedDataService : ISeedDataService
    {
        private readonly CinemaTicketContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Queue<string> Messages { get; set; } = new Queue<string>();

        public SeedDataService(CinemaTicketContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedDataAsync()
        {
            try
            {
                await SeedTheaters(_context.Theaters);
                await SeedMovies(_context.Movies);
                await SeedScreeningRooms(_context.ScreeningRooms);
                await SeedShowtimes(_context.Showtimes);
                await SeedSeats(_context.Seats);
                await SeedAppRoles();
                await SeedUsers();
                await SeedTicketBookings(_context.TicketBookings);
            }
            catch
            (Exception ex)
            {
                Messages.Enqueue(ex.Message);
            }
        }
        private async Task SeedTheaters(DbSet<Theater> dbset)
        {
            if (dbset.Any())
            {
                return;
            }
            else
            {
                var data = new List<Theater>
                {
                    new Theater
                    {
                        Id = -1,
                        Name = "Galaxy Tân Bình",
                        Location = "246 Nguyễn Hồng Đào, Q.Tân Bình, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -2,
                        Name = "Galaxy Nguyễn Du",
                        Location = "116 Nguyễn Du, Q.1, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -3,
                        Name = "Galaxy Kinh Dương Vương",
                        Location = "718bis Kinh Dương Vương, Q.6, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -4,
                        Name = "Galaxy Quang Trung",
                        Location = "Lầu 3, CoopMart Foodcosa, 304A Quang Trung, P.11, Q. Gò Vấp, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -5,
                        Name = "Galaxy Trung Chánh",
                        Location = "Trung Tâm Văn Hóa Quận 12, 09 Quốc lộ 22, P. Trung Mỹ Tây, Q.12, Tp. Hồ Chí Minh",
                    },
                     new Theater
                    {
                        Id = -6,
                        Name = "CGV Crescent Mall",
                        Location = "Lầu 5, Crescent Mall Đại lộ Nguyễn Văn Linh, Phú Mỹ Hưng, Q.7 Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -7,
                        Name = "CGV Celadon Tân Phú",
                        Location = "Lầu 3, Aeon Mall 30 Bờ Bao Tân Thắng, P. Sơn Kỳ, Q. Tân Phú, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -8,
                        Name = "CGV SC VivoCity",
                        Location = "Lầu 5, TTTM SC VivoCity, 1058 Nguyễn Văn Linh, Q.7, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -9,
                        Name = "CGV Vincom Đồng Khởi",
                        Location = "72 Lê Thánh Tôn, P. Bến Nghé, Q.1, Tp. Hồ Chí Minh",
                    },
                    new Theater
                    {
                        Id = -10,
                        Name = "CGV Pearl Plaza",
                        Location = "Tầng 5, Pearl Plaza, 561A Điện Biên Phủ, P.25, Q. Bình Thạnh, Tp. Hồ Chí Minh",
                    },

                };


                await _context.AddRangeAsync(data);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data Theater thành công");
            }
        }

        private async Task SeedMovies(DbSet<Movie> dbset)
        {
            if (dbset.Any())
            {
                return;
            }
            else
            {
                var data = new List<Movie>
                {
                    new Movie { Id = -1, Title = "Chamber Italian", Description = "A Fateful Reflection of a Moose And a Husband who must Overcome a Monkey in Nigeria", StatusId = MovieStatusEnum.NowShowing },
                    new Movie { Id = -2, Title = "Grosse Wonderful", Description = "A Epic Drama of a Cat And a Explorer who must Redeem a Moose in Australia", StatusId = MovieStatusEnum.NowShowing },
                    new Movie { Id = -3, Title = "Airport Pollock", Description = "A Epic Tale of a Moose And a Girl who must Confront a Monkey in Ancient India", StatusId = MovieStatusEnum.NowShowing },
                };

                await _context.AddRangeAsync(data);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data Movie thành công");
            }
        }

        private async Task SeedScreeningRooms(DbSet<ScreeningRoom> dbset)
        {
            if (dbset.Any())
            {
                return;
            }
            else
            {
                var data = new List<ScreeningRoom>
                {
                    new ScreeningRoom { Id = -1, CreatedDate = DateTime.UtcNow, Name = "Phòng A1", TheaterId = -1, ScreeningRoomTypeId = ScreeningRoomTypeEnum.Premium },
                    new ScreeningRoom { Id = -2, CreatedDate = DateTime.UtcNow, Name = "Phòng A2", TheaterId = -1, ScreeningRoomTypeId = ScreeningRoomTypeEnum.Premium },
                    new ScreeningRoom { Id = -3, CreatedDate = DateTime.UtcNow, Name = "Phòng A3", TheaterId = -1, ScreeningRoomTypeId = ScreeningRoomTypeEnum.Deluxe },
                    new ScreeningRoom { Id = -4, CreatedDate = DateTime.UtcNow, Name = "Phòng A4", TheaterId = -1, ScreeningRoomTypeId = ScreeningRoomTypeEnum.Deluxe },
                    new ScreeningRoom { Id = -5, CreatedDate = DateTime.UtcNow, Name = "Phòng A5", TheaterId = -1, ScreeningRoomTypeId = ScreeningRoomTypeEnum.Deluxe },
                    new ScreeningRoom { Id = -6, CreatedDate = DateTime.UtcNow, Name = "Phòng A6", TheaterId = -1, ScreeningRoomTypeId = ScreeningRoomTypeEnum.Deluxe },

                };



                await _context.AddRangeAsync(data);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data ScreeningRoom thành công");
            }
        }

        private async Task SeedShowtimes(DbSet<Showtime> dbset)
        {
            if (dbset.Any())
            {
                return;
            }
            else
            {
                var data = new List<Showtime>
                {
                    new Showtime { Id = -1, MovieId = -1, SccreeningRoomId = -1, ShowtimeScheduleId = ShowtimeScheduleEnum.Morning, Date = new DateOnly(2024, 1, 18) },
                    new Showtime { Id = -2, MovieId = -1, SccreeningRoomId = -1, ShowtimeScheduleId = ShowtimeScheduleEnum.Afternoon, Date = new DateOnly(2024, 1, 18) },
                    new Showtime { Id = -3, MovieId = -1, SccreeningRoomId = -1, ShowtimeScheduleId = ShowtimeScheduleEnum.Evening, Date = new DateOnly(2024, 1, 18) },
                    new Showtime { Id = -4, MovieId = -1, SccreeningRoomId = -1, ShowtimeScheduleId = ShowtimeScheduleEnum.Night, Date = new DateOnly(2024, 1, 18) },
                };



                await _context.AddRangeAsync(data);
                await _context.SaveChangesAsync();

                Messages.Enqueue("Seed data Showtime thành công");
            }
        }

        private async Task SeedSeats(DbSet<Seat> dbset)
        {
            if (dbset.Any())
            {
                return;
            }
            else
            {
                var shows = await _context.Showtimes.Include(s => s.ScreeningRoom).ToListAsync();

                foreach (var show in shows)
                {
                    if (show.ScreeningRoom is not null)
                    {
                        var list = new List<Seat>();

                        for (int i = 1; i <= show.ScreeningRoom.Capacity; i++)
                        {
                            list.Add(new Seat { SeatNumber = i, ShowtimeId = show.Id });
                        }

                        show.Seats = list;

                        await _context.SaveChangesAsync();
                    }
                }

                //int start = -1;
                //foreach (var room in rooms)
                //{
                //    var id = room.Id;

                //    var list = new List<Seat>();
                //    var numberOfSeats = room.Capacity;

                //    for (int i = 0; i < numberOfSeats; i++)
                //    {
                //        list.Add(new Seat { Id = start, ScreeningRoomId = id });
                //        start--;
                //    }

                //    await _context.AddRangeAsync(list);
                //    await _context.SaveChangesAsync();
                //}

                Messages.Enqueue("Seed data Seat thành công");
            }
        }

        private async Task SeedAppRoles()
        {
            if (_roleManager.Roles.Any())
            {
                return;
            }
            else
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = AppRoleEnum.Finance.ToString() },
                    new IdentityRole { Name = AppRoleEnum.Administrator.ToString() },
                    new IdentityRole { Name = AppRoleEnum.OfficeManager.ToString() },
                    new IdentityRole { Name = AppRoleEnum.TicketSeller.ToString() },
                };

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }

                Messages.Enqueue("Seed data Role thành công");
            }
        }

        private async Task SeedUsers()
        {
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                PhoneNumber = "0123456789",
                CreatedDate = DateTime.UtcNow,
            };

            var result = await _userManager.CreateAsync(admin, "Abc@123");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, AppRoleEnum.Administrator.ToString());
                Messages.Enqueue($"Seed data User {admin.UserName} thành công");
            }
            else
            {
                result.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }



            var manager = new ApplicationUser
            {
                UserName = "manager",
                Email = "manager@gmail.com",
                PhoneNumber = "0123456789",
                CreatedDate = DateTime.UtcNow,
            };

            var result1 = await _userManager.CreateAsync(manager, "Abc@123");

            if (result1.Succeeded)
            {
                await _userManager.AddToRoleAsync(manager, AppRoleEnum.OfficeManager.ToString());
                Messages.Enqueue($"Seed data User {manager.UserName} thành công");
            }
            else
            {
                result1.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }


            var ticketSeller = new ApplicationUser
            {
                UserName = "seller",
                Email = "seller@gmail.com",
                PhoneNumber = "0123456789",
                CreatedDate = DateTime.UtcNow,
            };

            var result2 = await _userManager.CreateAsync(ticketSeller, "Abc@123");

            if (result2.Succeeded)
            {
                await _userManager.AddToRoleAsync(ticketSeller, AppRoleEnum.TicketSeller.ToString());
                Messages.Enqueue($"Seed data User {ticketSeller.UserName} thành công");
            }
            else
            {
                result2.Errors.ToList().ForEach(error => Messages.Enqueue(error.Description));
                Messages.Enqueue("Seed data User thất bại");
            }
        }

        private async Task SeedTicketBookings(DbSet<Ticket> dbset)
        {
            if (dbset.Any())
            {
                return;
            }

            var bookings = new List<Ticket>
            {
                new Ticket
                {
                    Id = -1,
                    Price = 10,
                    TicketDetails = new List<TicketDetail>()
                    {
                        new TicketDetail {TicketId = -1, ShowtimeId = -1, SeatNumber = 1},
                        new TicketDetail {TicketId = -1, ShowtimeId = -1, SeatNumber = 2},
                        new TicketDetail {TicketId = -1, ShowtimeId = -1, SeatNumber = 3},
                    }
                },

                new Ticket
                {
                    Id = -2,
                    Price = 10,
                    TicketDetails = new List<TicketDetail>()
                    {
                        new TicketDetail {TicketId = -2, ShowtimeId = -1, SeatNumber = 4},
                        new TicketDetail {TicketId = -2, ShowtimeId = -1, SeatNumber = 5},
                        new TicketDetail {TicketId = -2, ShowtimeId = -1, SeatNumber = 6},
                    }
                },

                new Ticket
                {
                    Id = -3,
                    Price = 10,
                    TicketDetails = new List<TicketDetail>()
                    {
                        new TicketDetail {TicketId = -3, ShowtimeId = -1, SeatNumber = 7},
                        new TicketDetail {TicketId = -3, ShowtimeId = -1, SeatNumber = 8},
                        new TicketDetail {TicketId = -3, ShowtimeId = -1, SeatNumber = 9},
                    }
                },
            };

            await _context.AddRangeAsync(bookings);
            await _context.SaveChangesAsync();

            Messages.Enqueue("Seed data Bookings thành công");
        }
    }
}