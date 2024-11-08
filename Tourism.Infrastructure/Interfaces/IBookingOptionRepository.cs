using Tourism.Domain;

namespace Tourism.Infrastructure.Interfaces;

public interface IBookingOptionRepository
{
    Task<IEnumerable<BookingOption>> GetAll();
    Task<BookingOption> GetById(Guid id);
    Task<Guid> Insert(BookingOption bookingOption);
    Task Update(BookingOption bookingOption);
    Task Delete(Guid id);
}