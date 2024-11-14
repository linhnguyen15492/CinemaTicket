using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Enums.EnumClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Infrastructure.Context
{
    public class CinemaTicketContext : IdentityDbContext<ApplicationUser>
    {
        private readonly string _connectionString = "Server=localhost; Port=3306; User ID=root; Password=123456; Database=ticket";

        public CinemaTicketContext(DbContextOptions<CinemaTicketContext> options) : base(options)
        {
        }

        public required DbSet<Theater> Theaters { get; set; }
        public required DbSet<Showtime> Showtimes { get; set; }
        public required DbSet<Movie> Movies { get; set; }
        public required DbSet<ScreeningRoom> ScreeningRooms { get; set; }
        public required DbSet<TicketBooking> TicketBookings { get; set; }
        public required DbSet<TicketBookingDetail> TicketBookingDetails { get; set; }
        public required DbSet<BookingStatus> BookingStatus { get; set; }
        public required DbSet<MovieStatus> MovieStatuses { get; set; }
        public required DbSet<ScreeningRoomType> ScreeningRoomTypes { get; set; }
        public required DbSet<ShowtimeSchedule> ShowtimeSchedules { get; set; }
        public required DbSet<Department> Departments { get; set; }
        public required DbSet<CinemaSeat> CinemaSeats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Showtime>(entity =>
            {
                entity.HasAlternateKey(c => new { c.Date, c.ShowtimeScheduleId, c.SccreeningRoomId }); ;
            });

            builder.Entity<TicketBooking>(entity =>
            {
                entity.HasAlternateKey(c => new { c.Id, c.ShowtimeId });
            });

            builder.Entity<TicketBookingDetail>(entity =>
            {
                entity.HasAlternateKey(c => new { c.TicketBookingId, c.CinemaSeatId });
            });

            builder.Entity<CinemaSeat>().HasAlternateKey(c => new { c.Id, c.ScreeningRoomId });

            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName is not null)
                {
                    if (tableName.StartsWith("AspNet"))
                    {
                        entityType.SetTableName(tableName.Substring(6));
                    }
                }
            }
        }
    }
}
