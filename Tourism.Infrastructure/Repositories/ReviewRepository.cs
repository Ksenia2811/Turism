using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
{
    public ReviewRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<Review>> GetAll()
    {
        string sql = "select * from reviews";
        return await QueryAsync(sql);
    }

    public async Task<IEnumerable<Review>> GetAllByUserId(Guid id)
    {
        string sql = "select * from reviews where user_id = @id";
        return await QueryAsync(sql, new { id= id });
    }

    public async Task<IEnumerable<Review>> GetAllByTourId(Guid id)
    {
        string sql = "select * from reviews where tour_id = @id";
        return await QueryAsync(sql, new { id= id });
    }

    public async Task<Review> GetById(Guid id)
    {
       string sql = "select * from reviews where review_id = @id";
       return (await QueryAsync(sql, new { id= id })).FirstOrDefault();
    }

    public async Task<Guid> Insert(Review review)
    {
        Guid id = Guid.NewGuid();
        review.ReviewId = id;
        string sql = "insert into reviews (review_id, user_id, tour_id, rating, comment, created_at, status) values (@reviewId, @userId, @tourId, @rating, @comment, @createdAt, @status)";
        await ExecuteAsync(sql, review);
        return id;
    }

    public async Task Update(Review review)
    {
        string sql = "update reviews set user_id = @userId, tour_id = @tourId, rating = @rating, comment = @comment, created_at = @createdAt, status= @status where review_id = @reviewId";
        await ExecuteAsync(sql, review);
    }

    public async Task Delete(Guid id)
    {
       string sql = "delete from reviews where review_id = @id";
       await ExecuteAsync(sql, new { id = id });
    }
}