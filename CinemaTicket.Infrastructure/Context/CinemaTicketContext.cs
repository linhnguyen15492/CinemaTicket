using App.Core.Entities;
using App.Core.Enums.EnumClasses;
using CinemaTicket.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicket.Infrastructure.Context
{
    public class CinemaTicketContext : DbContext
    {
        private readonly string _connectionString = "Server=localhost; Port=3306; User ID=root; Password=123456; Database=ticket";

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ScreeningRoom> ScreeningRooms { get; set; }
        public DbSet<TicketBooking> TicketBookings { get; set; }
        public DbSet<TicketBookingDetail> TicketBookingDetails { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<MovieStatus> MovieStatuses { get; set; }
        public DbSet<ScreeningRoomType> ScreeningRoomTypes { get; set; }
        public DbSet<ShowtimeSchedule> ShowtimeSchedules { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CinemaSeat> CinemaSeats { get; set; }

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
