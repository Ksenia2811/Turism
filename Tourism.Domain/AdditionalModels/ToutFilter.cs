using System.Runtime.InteropServices;

namespace Tourism.Domain.AdditionalModels;

public class ToutFilter
{
     public List<string>? Countries { get; set; }
     public int? MinDuration { get; set; }
     public int? MaxDuration { get; set; }
     public decimal? MinPrice { get; set; }
     public decimal? MaxPrice { get; set; }
     public string? SortBy { get; set; }
     public string? SortDirection { get; set; }
}