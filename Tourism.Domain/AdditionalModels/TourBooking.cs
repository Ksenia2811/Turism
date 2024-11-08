namespace Tourism.Domain.AdditionalModels;

public class TourBooking
{
    public Guid UserId { get; set; }
    public Guid TourId { get; set; }
    public List<Guid> Options { get; set; }
    
}