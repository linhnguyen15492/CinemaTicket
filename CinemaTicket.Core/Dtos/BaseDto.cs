namespace CinemaTicket.Core.Dtos
{
    public abstract class BaseDto : IDto
    {
        public virtual int Id { get; set; }
    }
}