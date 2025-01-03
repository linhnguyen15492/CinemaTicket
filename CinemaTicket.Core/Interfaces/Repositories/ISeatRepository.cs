﻿using CinemaTicket.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Interfaces.Repositories
{
    public interface ISeatRepository : IRepository<Seat>
    {
        Task<bool> UpdateSeatStatusAsync(int seatNumber, int showtimeId);
    }
}
