using Tourism.Domain;
using Tourism.Domain.Models;

namespace Tourism.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task<User> GetById(Guid id);
    Task<Guid> Insert(User user);
    Task Update(User user);
    Task Delete(Guid id);
}