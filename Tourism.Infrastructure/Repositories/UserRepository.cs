using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Domain.Models;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        var sql = "select * from users";
        return await QueryAsync(sql);
    }

    public async Task<User> GetByEmail(string email)
    {
        var sql = "select * from users where email = @email";
        return (await QueryAsync(sql, new { email = email })).FirstOrDefault();
    }

    public async Task<User> GetById(Guid id)
    {
        var sql = "select * from users where user_id = @id";
        return (await QueryAsync(sql, new { id = id })).FirstOrDefault();
    }

    public async Task<Guid> Insert(User user)
    {
        var id = Guid.NewGuid();
        user.UserId = id;
        string sql =
            "insert into users (user_id, name, email, password, created_at, role) values (@userId, @name, @email, @password, @createdAt, @role)";
        await ExecuteAsync(sql, user);
        return id;
    }

    public async Task Update(User user)
    {
        string sql = "update users set name = @name, email = @email, password = @password, created_at = @createdAt, role = @role where user_id = @userId";
        await ExecuteAsync(sql, user);
    }

    public async Task Delete(Guid id)
    {
       string sql = "delete from users where user_id = @userId";
       await ExecuteAsync(sql, new { userId = id });
    }
}