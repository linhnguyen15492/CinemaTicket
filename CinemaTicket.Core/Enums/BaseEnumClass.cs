using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CinemaTicket.Core.Extensions;

namespace CinemaTicket.Core.Enums
{
    public class BaseEnumClass<TEnum> where TEnum : Enum
    {
        [MaxLength(100)]
        public string Description { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TEnum Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        private BaseEnumClass(TEnum @enum)
        {
            Id = @enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        public static implicit operator BaseEnumClass<TEnum>(TEnum @enum) => new BaseEnumClass<TEnum>(@enum);

        public static implicit operator TEnum(BaseEnumClass<TEnum> faculty) => faculty.Id;
    }
}