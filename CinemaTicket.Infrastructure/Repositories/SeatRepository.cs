using CinemaTicket.Core.Entities;
using CinemaTicket.Core.Interfaces.Repositories;
using CinemaTicket.Infrastructure.Context;
using CinemaTicket.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Infrastructure.Repositories
{
    public class SeatRepository : GenericRepository<Seat>, ISeatRepository
    {
        public SeatRepository(CinemaTicketContext context) : base(context)
        {
        }
    }
}
