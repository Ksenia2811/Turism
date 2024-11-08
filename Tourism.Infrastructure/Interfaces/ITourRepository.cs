using Tourism.Domain;
using Tourism.Domain.AdditionalModels;

namespace Tourism.Infrastructure.Interfaces;

public interface ITourRepository
{
    Task<IEnumerable<Tour>> GetAll();
    Task<IEnumerable<Tour>> GetFiltered(ToutFilter filter);
    Task<Tour> GetById(Guid id);
    Task<Guid> Insert(Tour tour);
    Task Update(Tour tour);
    Task Delete(Guid id);
}