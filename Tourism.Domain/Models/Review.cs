namespace Tourism.Domain;

public class Review
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }
    public Guid TourId { get; set; }
    public double Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
}