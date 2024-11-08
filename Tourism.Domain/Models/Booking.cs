namespace Tourism.Domain;

public class Booking
{
    public Guid BookingId { get; set; }
    public Guid UserId { get; set; }
    public Guid TourId { get; set; }
    public decimal TotalPrice { get; set; }
    public string BookingStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}