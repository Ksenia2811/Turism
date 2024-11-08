using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Tourism.Infrastructure.Repositories;

public abstract class RepositoryBase<T>
{
    private readonly string _connectionString;

    protected RepositoryBase(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    protected async Task<IEnumerable<T>> QueryAsync(string sql, object parameters = null)
    {
        using (IDbConnection db = new NpgsqlConnection(_connectionString))
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            return await db.QueryAsync<T>(sql, parameters);
        }
    }

    protected async Task<int> ExecuteAsync(string sql, object parameters = null)
    {
        using (IDbConnection db = new NpgsqlConnection(_connectionString))
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            return await db.ExecuteAsync(sql, parameters);
        }
    }
}