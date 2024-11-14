using CinemaTicket.Core.Entities;
using CinemaTicket.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Infrastructure.Repositories
{
    public class ScreeningRoomRepository : GenericRepository<ScreeningRoom>
    {
        public ScreeningRoomRepository(DbContext context) : base(context)
        {
        }
    }
}
