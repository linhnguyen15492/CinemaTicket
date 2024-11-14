using System.ComponentModel;

namespace CinemaTicket.Core.Enums
{
    public enum MovieStatusEnum
    {
        [Description("Phim đang chiếu")] NowShowing = 0,
        [Description("Phim sắp chiếu")] ComingSoon = 1,
        [Description("Kết thúc công chiếu")] NoLongerShowing = 2
    }
}
