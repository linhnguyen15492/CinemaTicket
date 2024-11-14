using CinemaTicket.Core.Enums.EnumClasses;
using CinemaTicket.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Entities
{
    public class ScreeningRoom : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public int Capacity
        {
            get
            {
                return ScreeningRoomTypeId == ScreeningRoomTypeEnum.Deluxe ? 60 : 100;
            }

            private set { }
        }

        public ScreeningRoomTypeEnum ScreeningRoomTypeId { get; set; }
        [ForeignKey("ScreeningRoomTypeId")]
        public ScreeningRoomType ScreeningRoomType { get; set; } = default!;

        [Required]
        public int TheaterId { get; set; }
        [ForeignKey("TheaterId")]
        public Theater Theater { get; set; } = default!;
    }
}
