namespace Tourism.Domain.AdditionalModels;

public class TourInfo
{
    public Tour TourData { get; set; }
    public Dictionary<string, List<TourOption>> TourOptions { get; set; }

    public double Rating { get; set; }
}