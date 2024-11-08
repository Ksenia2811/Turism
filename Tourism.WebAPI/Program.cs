using Tourism.Infrastructure.Interfaces;
using Tourism.Infrastructure.Repositories;
using Tourism.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddSingleton<IBookingOptionRepository, BookingOptionRepository>();
builder.Services.AddSingleton<IReviewRepository, ReviewRepository>();
builder.Services.AddSingleton<ITourRepository, TourRepository>();
builder.Services.AddSingleton<ITourOptionRepository, TourOptionRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserFavoriteRepository, UserFavoriteRepository>();


builder.Services.AddSingleton<ErrorHandlingMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();