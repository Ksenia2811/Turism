using Tourism.Domain;

namespace Tourism.Infrastructure.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetAll();
    Task<IEnumerable<Booking>> GetByUserId(Guid id);
    Task<Booking> GetById(Guid id);
    Task<Guid> Insert(Booking booking);
    Task Update(Booking booking);
    Task Delete(Guid id);
}