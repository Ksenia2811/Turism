using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Domain;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingOptionController : ControllerBase
    {
        private IBookingOptionRepository _bookingOptionRepository;

        public BookingOptionController(IBookingOptionRepository bookingOptionRepository)
        {
            _bookingOptionRepository = bookingOptionRepository;
        }

        // GET: api/BookingOption
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bookingOptionRepository.GetAll());
        }

        // GET: api/BookingOption/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _bookingOptionRepository.GetById(id));
        }


        // POST: api/BookingOption
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] BookingOption bookingOption)
        {
            return Ok(await _bookingOptionRepository.Insert(bookingOption));
        }

        // PUT: api/BookingOption/5
        [HttpPut]
        public async Task<ActionResult> Update ([FromBody] BookingOption bookingOption)
        {
            await _bookingOptionRepository.Update(bookingOption);
            return Ok();
        }

        // DELETE: api/BookingOption/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _bookingOptionRepository.Delete(id);
            return Ok();
        }
    }
}
