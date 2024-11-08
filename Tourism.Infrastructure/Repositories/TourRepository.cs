using System.Text;
using Microsoft.Extensions.Configuration;
using Tourism.Domain;
using Tourism.Domain.AdditionalModels;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.Infrastructure.Repositories;

// Класс TourRepository отвечает за взаимодействие с базой данных для сущности Tour.
public class TourRepository : RepositoryBase<Tour>, ITourRepository
{
    // Конструктор класса, принимающий конфигурацию для настройки подключения к базе данных.
    public TourRepository(IConfiguration configuration) : base(configuration)
    {
    }

    // Метод для получения списка туров с применением фильтров.
    public async Task<IEnumerable<Tour>> GetFiltered(ToutFilter filter)
    {
        // Начинаем с базового SQL-запроса.
        var sqlBuilder = new StringBuilder("SELECT * FROM tours WHERE 1=1");
        
        // Проверяем наличие фильтра по странам и добавляем соответствующее условие.
        if (filter.Countries != null && filter.Countries.Count > 0)
        {
            sqlBuilder.Append(" AND country = ANY(@countries)");
        }

        // Добавляем фильтр по минимальной длительности.
        if (filter.MinDuration is > 0)
        {
            sqlBuilder.Append(" AND duration >= @minDuration");
        }

        // Добавляем фильтр по максимальной длительности, если он валиден.
        if (filter.MaxDuration is > 0 && filter.MinDuration <= filter.MaxDuration)
        {
            sqlBuilder.Append(" AND duration <= @maxDuration");
        }
        
        // Добавляем фильтр по минимальной цене.
        if (filter.MinPrice is > 0)
        {
            sqlBuilder.Append(" AND basic_price >= @minPrice::money");
        }
        
        // Добавляем фильтр по максимальной цене, если он валиден.
        if (filter.MaxPrice is > 0 && filter.MinPrice <= filter.MaxPrice)
        {
            sqlBuilder.Append(" AND basic_price <= @maxPrice::money");
        }

        // Добавляем сортировку, если указаны параметры сортировки.
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            var validSortColumns = new List<string> { "basic_price", "name", "country", "duration", "created_at" };
            if (validSortColumns.Contains(filter.SortBy.ToLower()))
            {
                sqlBuilder.Append($" ORDER BY {filter.SortBy}");
                // Устанавливаем направление сортировки.
                if (filter.SortDirection?.ToLower() == "desc")
                {
                    sqlBuilder.Append(" DESC");
                }
                else
                {
                    sqlBuilder.Append(" ASC");
                }
            }
        }  
        
        // Выводим сформированный SQL-запрос в консоль для отладки.
        Console.Error.WriteLine(sqlBuilder.ToString());
        
        // Выполняем SQL-запрос с параметрами и возвращаем результаты.
        return await QueryAsync(sqlBuilder.ToString(), filter);
    }

    // Метод для получения всех туров без фильтров.
    public async Task<IEnumerable<Tour>> GetAll()
    {
        string sql = "select * from tours";
        return await QueryAsync(sql);
    }

    // Метод для получения тура по его идентификатору.
    public async Task<Tour> GetById(Guid id)
    {
        string sql = "select * from tours where tour_id = @id";
        return (await QueryAsync(sql, new { id = id })).FirstOrDefault();
    }

    // Метод для вставки нового тура в базу данных.
    public async Task<Guid> Insert(Tour tour)
    {
       Guid id = Guid.NewGuid(); // Генерируем новый идентификатор для тура.
       tour.TourId = id; // Присваиваем идентификатор объекту тура.
       // SQL-запрос для вставки нового тура.
       string sql = "insert into tours (tour_id, name, description, country, duration, created_at, basic_price) values (@tourId, @name, @description, @country, @duration, @createdAt, @basicPrice)";
       await ExecuteAsync(sql, tour); // Выполняем запрос.
       return id; // Возвращаем идентификатор нового тура.
    }

    // Метод для обновления существующего тура.
    public async Task Update(Tour tour)
    {
        // SQL-запрос для обновления тура.
        string sql = "update tours SET name= @name, description = @description, country = @country, duration = @duration, created_at = @createdAt, basic_price = @basicPrice where tour_id = @tourId";
        await ExecuteAsync(sql, tour); // Выполняем запрос.
    }

    // Метод для удаления тура по его идентификатору.
    public async Task Delete(Guid id)
    {
       // SQL-запрос для удаления тура .
       string sql = "delete from tours where tour_id = @id";
       await ExecuteAsync(sql, new { id = id }); // Выполняем запрос.
    }
}