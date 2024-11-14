using System.ComponentModel;
using App.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace CinemaTicket.Core.Extensions
{
    public static class AppExtensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item)
        => item.GetType()
               .GetField(item.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false)
               .Cast<DescriptionAttribute>()
        .FirstOrDefault()?.Description ?? string.Empty;


        public static void SeedEnumValues<T, TEnum>(this IDbSet<T> dbSet, Func<TEnum, T> converter)
            where T : class => Enum.GetValues(typeof(TEnum))
                                   .Cast<object>()
                                   .Select(value => converter((TEnum)value))
                                   .ToList()
                                   .ForEach(instance => dbSet.AddOrUpdate(instance));


        public static void SeedEnum(this ModelBuilder modelBuilder)
        {
            // to populate enum
            foreach (var e in Enum.GetValues(typeof(BookingStatusEnum)).Cast<BookingStatusEnum>())
            {
                modelBuilder.Entity<BookingStatus>().HasData(new BookingStatus
                {
                    Id = e,
                    Name = e.ToString(),
                    Description = e.GetEnumDescription()
                });
            }

            // to populate enum
            foreach (var e in Enum.GetValues(typeof(MovieStatusEnum)).Cast<MovieStatusEnum>())
            {
                modelBuilder.Entity<MovieStatus>().HasData(new MovieStatus
                {
                    Id = e,
                    Name = e.ToString(),
                    Description = e.GetEnumDescription()
                });
            }

            foreach (var e in Enum.GetValues(typeof(ScreeningRoomTypeEnum)).Cast<ScreeningRoomTypeEnum>())
            {
                modelBuilder.Entity<ScreeningRoomType>().HasData(new ScreeningRoomType
                {
                    Id = e,
                    Name = e.ToString(),
                    Description = e.GetEnumDescription()
                });
            }

            foreach (var e in Enum.GetValues(typeof(ShowtimeScheduleEnum)).Cast<ShowtimeScheduleEnum>())
            {
                modelBuilder.Entity<ShowtimeSchedule>().HasData(new ShowtimeSchedule
                {
                    Id = e,
                    Name = e.ToString(),
                    Description = e.GetEnumDescription()
                });
            }

            foreach (var e in Enum.GetValues(typeof(DepartmentEnum)).Cast<DepartmentEnum>())
            {
                modelBuilder.Entity<Department>().HasData(new Department
                {
                    Id = e,
                    Name = e.ToString(),
                    Description = e.GetEnumDescription()
                });
            }
        }
    }
}
