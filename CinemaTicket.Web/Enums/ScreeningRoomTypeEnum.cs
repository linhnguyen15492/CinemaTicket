using System.ComponentModel;

namespace CinemaTicket.Core.Enums
{
    public enum ScreeningRoomTypeEnum
    {
        [Description("Phòng loại 2 có 60 ghế")] Deluxe = 0,
        [Description("Phòng loại 1 có 100 ghế")] Premium = 1
    }
}
