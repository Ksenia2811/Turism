using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

public class UserFavoriteRepository : RepositoryBase<UserFavorite>, IUserFavoriteRepository
{
    public UserFavoriteRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<UserFavorite>> GetAll()
    {
        string sql = "select * from user_favorites";
        return await QueryAsync(sql);
    }

    public async Task<IEnumerable<UserFavorite>> GetByUserId(Guid id)
    {
        string sql = "select * from user_favorites where user_id=@id";
        return await QueryAsync(sql, new { id = id });
    }

    public async Task<UserFavorite> GetById(Guid id)
    {
        string sql = "select * from user_favorites where favorite_id=@id";
        return (await QueryAsync(sql, new { id = id })).FirstOrDefault();
    }

    public async Task<Guid> Insert(UserFavorite userFavorite)
    {
        Guid id = Guid.NewGuid();
        userFavorite.FavoriteId = id;
        string sql =
            "insert into user_favorites (favorite_id, user_id, tour_id, created_at) VALUES (@favoriteId, @userId, @tourId, @createAt)";
        await QueryAsync(sql, userFavorite);
        return id;
    }

    public async Task Update(UserFavorite userFavorite)
    {
        string sql =
            "update user_favorites set user_id = @userId, tour_id = @tourId, created_at = @createAt WHERE favorite_id=@favoriteId";
        await QueryAsync(sql, userFavorite);
    }

    public async Task Delete(Guid id)
    {
        string sql = "delete from user_favorites where favorite_id=@id";
        await QueryAsync(sql, new { id = id });
    }
}