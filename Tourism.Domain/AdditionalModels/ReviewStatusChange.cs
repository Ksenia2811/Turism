namespace Tourism.Domain.AdditionalModels;

public class ReviewStatusChange
{
    public Guid ReviewId { get; set; }
    public string NewStatus { get; set; }
}