using CinemaTicket.Core.Dtos;
using CinemaTicket.Core.Entities;

namespace CinemaTicket.Core.Mappers
{
    public static class UserMappers
    {
        public static ApplicationUserDto ToUserDto(this ApplicationUser user)
        {
            return new ApplicationUserDto
            {
                Email = user.Email!,
                Username = user?.UserName!
            };
        }
    }
}
