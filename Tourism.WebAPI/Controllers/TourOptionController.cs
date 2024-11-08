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
    public class TourOptionController : ControllerBase
    {
        private ITourOptionRepository _tourOptionRepository;

        public TourOptionController(ITourOptionRepository tourOptionRepository)
        {
            _tourOptionRepository = tourOptionRepository;
        }

        // GET: api/TourOption
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _tourOptionRepository.GetAll());
        }

        // GET: api/TourOption/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _tourOptionRepository.GetById(id));
        }

        // POST: api/TourOption
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] TourOption tourOption)
        {
            return Ok(await _tourOptionRepository.Insert(tourOption));
        }

        // PUT: api/TourOption/5
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] TourOption tourOption)
        {
            await _tourOptionRepository.Update(tourOption);
            return Ok();
        }

        // DELETE: api/TourOption/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _tourOptionRepository.Delete(id);
            return Ok();
        }
    }
}
