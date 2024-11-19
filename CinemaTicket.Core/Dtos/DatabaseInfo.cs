using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Dtos
{
    public class DatabaseInfo
    {
        public bool CanConnect { get; set; }
        public string DatabaseName { get; set; } = string.Empty;
        public List<string>? TableNames { get; set; }
    }
}
