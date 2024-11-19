using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicket.Core.Interfaces.Services
{
    public interface ISalesService
    {
        Task<object> GetSalesByShow();
        Task<object> GetSalesByMovie();
    }
}
