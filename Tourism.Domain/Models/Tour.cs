namespace Tourism.Domain;

public class Tour
{
    public Guid TourId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public int Duration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal BasicPrice { get; set; }
}
