namespace Tourism.Domain;

public class BookingOption
{
   public Guid BookingOptionId { get; set; }
   public Guid BookingId { get; set; }
   public Guid OptionId { get; set; }
   public DateTime CreatedAt { get; set; } = DateTime.Now;
   
   
}