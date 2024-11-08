using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<Booking>> GetAll()
    {
        string sql = "select * from bookings";
        return await QueryAsync(sql);
    }

    public async Task<IEnumerable<Booking>> GetByUserId(Guid id)
    {
        string sql = "select * from bookings where user_id = @id";
        return await QueryAsync(sql, new { id = id });
    }

    public async Task<Booking> GetById(Guid id)
    {
        string sql = "select * from bookings where booking_id = @id";
        return (await QueryAsync(sql, new { id = id })).FirstOrDefault();
    }

    public async Task<Guid> Insert(Booking booking)
    {
        var id = Guid.NewGuid();
        booking.BookingId = id;
        string sql =
            "INSERT into bookings (booking_id, user_id, tour_id, total_price, booking_status, created_at) values (@bookingId,@userId,@tourId,@totalPrice, @bookingStatus,@createdAt)";
        await ExecuteAsync(sql, booking);
        return id;
    }

    public async Task Update(Booking booking)
    {
        string sql =
            "update bookings set user_id = @userId, tour_id = @tourId, total_price = @totalPrice, booking_status = @bookingStatus, created_at = @createdAt where booking_id = @id";
        await ExecuteAsync(sql, booking);
    }

    public async Task Delete(Guid id)
    {
        string sql = "delete from bookings where booking_id = @bookingId";
        await ExecuteAsync(sql, new { bookingId = id });
    }
}