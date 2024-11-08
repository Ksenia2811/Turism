namespace Tourism.Domain;

public class TourOption
{
    public Guid OptionId { get; set; }
    public Guid TourId { get; set; }
    public string OptionType { get; set; }
    public string Description { get; set; }
    public bool DefaultValue { get; set; }
    public decimal AdditionalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}