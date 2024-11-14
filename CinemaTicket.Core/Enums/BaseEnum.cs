using CinemaTicket.Core.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicket.Core.Enums
{
    public class BaseEnum<TEnum> where TEnum : Enum
    {

        private BaseEnum(TEnum @enum)
        {
            Id = @enum;
            Name = @enum.ToString();
            Description = @enum.GetEnumDescription();
        }

        protected BaseEnum() { } //For EF

        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TEnum Id { get; set; } = default!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;


        public static implicit operator BaseEnum<TEnum>(TEnum @enum) => new BaseEnum<TEnum>(@enum);

        public static implicit operator TEnum(BaseEnum<TEnum> baseEnum) => baseEnum.Id;
    }
}