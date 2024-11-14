using App.Core.Entities;
using App.Core.Interfaces.Repositories;
using App.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class TicketRepository : GenericRepository<TicketBooking>, ITicketRepository
    {
        public TicketRepository(DbContext context) : base(context)
        {
        }
    }
}
