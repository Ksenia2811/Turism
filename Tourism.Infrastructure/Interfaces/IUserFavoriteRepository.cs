using Tourism.Domain;

namespace Tourism.Infrastructure.Interfaces;

public interface IUserFavoriteRepository
{
    Task<IEnumerable<UserFavorite>> GetAll();
    Task<IEnumerable<UserFavorite>> GetByUserId(Guid id);
    Task<UserFavorite> GetById(Guid id);
    Task<Guid> Insert(UserFavorite userFavorite);
    Task Update(UserFavorite userFavorite);
    Task Delete(Guid id);
}