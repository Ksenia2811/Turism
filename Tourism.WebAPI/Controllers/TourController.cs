using Microsoft.AspNetCore.Mvc;
using Tourism.Domain;
using Tourism.Domain.AdditionalModels;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.WebAPI.Controllers
{
    // Атрибут Route определяет маршрут для всех действий контроллера.
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        // Объявляем зависимости репозиториев для работы с турами, опциями туров и отзывами.
        private ITourRepository _tourRepository;
        private ITourOptionRepository _tourOptionRepository;
        private IReviewRepository _reviewRepository;

        // Конструктор контроллера, принимающий необходимые репозитории через Dependency Injection.
        public TourController(ITourRepository tourRepository, ITourOptionRepository tourOptionRepository,
            IReviewRepository reviewRepository)
        {
            _tourRepository = tourRepository;
            _tourOptionRepository = tourOptionRepository;
            _reviewRepository = reviewRepository;
        }

        // Метод для получения информации о туре по его идентификатору.
        [HttpGet("id")]
        public async Task<IActionResult> GetTourInfo(Guid id)
        {
            // Получаем данные о туре.
            var tourData = await _tourRepository.GetById(id);
            // Получаем опции для данного тура.
            var tourOptionData = await _tourOptionRepository.GetByTourId(tourData.TourId);
            // Получаем отзывы о туре.
            var reviews = (await _reviewRepository.GetAllByTourId(id)).ToList();
            // Рассчитываем средний рейтинг на основе отзывов.
            var rating = reviews.Count > 0 ? reviews.Sum(r => r.Rating) / reviews.Count : 0;
            // Возвращаем информацию о туре, опциях и рейтинге.
            return Ok(new TourInfo
            {
                TourData = tourData,
                TourOptions = tourOptionData.GroupBy(o => o.OptionType)
                    .ToDictionary(o => o.Key, o => o.ToList()),
                Rating = rating
            });
        }

        // Метод для получения списка туров с применением фильтров.
        [HttpGet]
        public async Task<IActionResult> GetTours(
            [FromQuery] string? countries,
            [FromQuery] int? minDuration,
            [FromQuery] int? maxDuration,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection)
        {
            // Создаем объект фильтра и передаем его в репозиторий для получения отфильтрованных туров.
            return Ok(await _tourRepository.GetFiltered(new ToutFilter()
            {
                Countries = countries?.Split(',').ToList(),
                MinDuration = minDuration,
                MaxDuration = maxDuration,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy,
                SortDirection = sortDirection
            }));
        }

        // Метод для получения всех туров без фильтров.
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _tourRepository.GetAll());
        }

        // Метод для получения тура по его идентификатору.
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _tourRepository.GetById(id));
        }

        // Метод для вставки нового тура в базу данных.
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] Tour tour)
        {
            return Ok(await _tourRepository.Insert(tour));
        }

        // Метод для обновления существующего тура.
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Tour tour)
        {
            await _tourRepository.Update(tour);
            return Ok();
        }

        // Метод для удаления тура по его идентификатору.
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _tourRepository.Delete(id);
            return Ok();
        }
    }
}