using System.ComponentModel;

namespace CinemaTicket.Core.Enums
{
    public enum DepartmentEnum
    {
        [Description("Bộ phận bán vé")] BookingOffice = 0,
        [Description("Bộ phận quản lý")] Management = 1
    }
}
