using Tourism.Domain;

namespace Tourism.Infrastructure.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAll();

    Task<IEnumerable<Review>> GetAllByUserId(Guid userId);
    Task<IEnumerable<Review>> GetAllByTourId(Guid tourId);
    Task<Review> GetById(Guid id);
    Task<Guid> Insert(Review review);
    Task Update(Review review);
    Task Delete(Guid id);
}