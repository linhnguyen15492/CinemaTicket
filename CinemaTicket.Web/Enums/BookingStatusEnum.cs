using System.ComponentModel;

namespace CinemaTicket.Web.Enums
{
    public enum BookingStatusEnum
    {
        [Description("Trạng thái pending, chờ kiểm tra")] Pending = 0,
        [Description("Trạng thái confirmed sau khi kiểm tra thành công")] Confirmed = 1,
        [Description("Đã thanh toán thành công")] Paid = 2,
        [Description("Đơn đã cancel")] Cancelled = 3
    }
}
