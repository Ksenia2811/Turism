using Tourism.Domain;

namespace Tourism.Infrastructure.Interfaces;

public interface ITourOptionRepository
{
    Task<IEnumerable<TourOption>> GetAll();
    Task<IEnumerable<TourOption>> GetByTourId(Guid id);
    Task<TourOption> GetById(Guid id);
    Task<Guid> Insert(TourOption tourOption);
    Task Update(TourOption tourOption);
    Task Delete(Guid id);
}