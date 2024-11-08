namespace Tourism.Domain;

public class UserFavorite
{
    public Guid FavoriteId { get; set; }
    public Guid UserId { get; set; }
    public Guid TourId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}