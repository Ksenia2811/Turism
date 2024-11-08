using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

public class BookingOptionRepository : RepositoryBase<BookingOption>, IBookingOptionRepository
{
    public BookingOptionRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<BookingOption>> GetAll()
    {
        var sql = "select * from booking_options";
        return await QueryAsync(sql);
    }

    public async Task<BookingOption> GetById(Guid id)
    {
        var sql = "select * from booking_options where booking_id = @id";
        return (await QueryAsync(sql, new { id = id })).FirstOrDefault();
    }

    public async Task<Guid> Insert(BookingOption bookingOption)
    {
        var id = Guid.NewGuid();
        bookingOption.BookingOptionId = id;
        string sql =
            "insert into booking_options (booking_option_id, booking_id, option_id, created_at) values (@bookingOptionId, @bookingId, @optionId, @createdAt)";
        await ExecuteAsync(sql, bookingOption);
        return id;
    }

    public async Task Update(BookingOption bookingOption)
    {
        string sql = "update booking_options set booking_id = @bookingId, option_id = @optionId where booking_option_id = @bookingOptionId ";
        await ExecuteAsync(sql, bookingOption);
    }

    public async Task Delete(Guid id)
    {
        string sql = "delete from booking_options where booking_id = @id";
        await ExecuteAsync(sql, new { id = id });
    }
}