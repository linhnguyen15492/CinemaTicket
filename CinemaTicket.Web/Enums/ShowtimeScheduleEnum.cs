using System.ComponentModel;

namespace CinemaTicket.Core.Enums
{
    public enum ShowtimeScheduleEnum
    {
        [Description("9h00-11h00")] Morning = 0,
        [Description("14h00-16h00")] Afternoon = 1,
        [Description("18h00-20h00")] Evening = 2,
        [Description("20h30-22h00")] Night = 3
    }
}
