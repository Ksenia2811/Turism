using Microsoft.AspNetCore.Mvc;
using Tourism.Domain;
using Tourism.Domain.AdditionalModels;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingOptionRepository _bookingOptionRepository;
        private readonly ITourOptionRepository _tourOptionRepository;
        private readonly ITourRepository _tourRepository;

        public BookingController(IBookingRepository bookingRepository, IBookingOptionRepository bookingOptionRepository,
            ITourOptionRepository tourOptionRepository, ITourRepository tourRepository)
        {
            _bookingRepository = bookingRepository;
            _bookingOptionRepository = bookingOptionRepository;
            _tourOptionRepository = tourOptionRepository;
            _tourRepository = tourRepository;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bookingRepository.GetAll());
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllByUserId(Guid id)
        {
            return Ok(await _bookingRepository.GetByUserId(id));
        }
        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _bookingRepository.GetById(id));
        }


        [HttpPost]
        public async Task<ActionResult> BookTour([FromBody] TourBooking model)
        {
            var basicPrice = (await _tourRepository.GetById(model.TourId)).BasicPrice;
            // Создаем список задач для получения дополнительных опций
            var tasks = model.Options.Select(el => _tourOptionRepository.GetById(el));
            // Ожидаем завершения всех задач и получаем результаты
            var options = await Task.WhenAll(tasks);
            var bookingId = await _bookingRepository.Insert(new Booking()
            {
                BookingId = Guid.Empty,
                BookingStatus = "Created",
                CreatedAt = DateTime.Now,
                TotalPrice = basicPrice + options.Sum(price => price.AdditionalPrice),
                TourId = model.TourId,
                UserId = model.UserId,
            });
            var bookingOptions = options.Select(el => _bookingOptionRepository.Insert(new BookingOption()
            {
                BookingId = bookingId,
                BookingOptionId = Guid.Empty,
                OptionId = el.OptionId,
                CreatedAt = DateTime.Now,
            }));
            await Task.WhenAll(bookingOptions);
            return Ok(bookingId);
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] Booking booking)
        {
            return Ok(await _bookingRepository.Insert(booking));
        }

        // POST: api/Booking/5
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Booking booking)
        {
            await _bookingRepository.Update(booking);
            return Ok();
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _bookingRepository.Delete(id);
            return Ok();
        }
    }
}