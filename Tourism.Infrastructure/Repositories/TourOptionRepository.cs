using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

public class TourOptionRepository : RepositoryBase<TourOption>, ITourOptionRepository
{
    public TourOptionRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<TourOption>> GetAll()
    {
        string sql = "select * from tour_options";
        return await QueryAsync(sql);
    }
    public async Task<IEnumerable<TourOption>> GetByTourId(Guid id)
    {
        string sql = "select * from tour_options where tour_id = @id";
        return (await QueryAsync(sql, new { id = id }));
    }
    public async Task<TourOption> GetById(Guid id)
    {
        string sql = "select * from tour_options where option_id = @id";
        return (await QueryAsync(sql, new { id = id })).FirstOrDefault();
    }

    public async Task<Guid> Insert(TourOption tourOption)
    {
        Guid id = Guid.NewGuid();
        tourOption.OptionId = id;

        string sql =
            "insert into tour_options (option_id, tour_id, option_type, description, default_value, additional_price, created_at) VALUES (@optionId, @tourId, @optionType, @description, @defaultValue, @additionalPrice, @createdAt) ";
        await ExecuteAsync(sql, tourOption);
        return id;
    }

    public async Task Update(TourOption tourOption)
    {
        string sql =
            "update tour_options set tour_id = @tourId, option_type = @optionType, description = @description, default_value = @defaultValue, additional_price = @additionalPrice, created_at = @createdAt where option_id = @optionId";
        await ExecuteAsync(sql, tourOption);
    }
    

    public async Task Delete(Guid id)
    {
        string sql = "delete from tour_options where option_id = @id";
        await ExecuteAsync(sql, new { id = id });
    }
}