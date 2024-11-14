using App.Core.Entities;
using App.Infrastructure.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class ScreeningRoomRepository : GenericRepository<ScreeningRoom>
    {
        public ScreeningRoomRepository(DbContext context) : base(context)
        {
        }
    }
}
