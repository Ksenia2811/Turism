using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Domain;
using Tourism.Domain.AdditionalModels;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok( await _reviewRepository.GetAll());
        }
        
        
        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByUserId(Guid id)
        {
            return Ok(await _reviewRepository.GetAllByUserId(id));
        }
        
        
        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByTourId(Guid id)
        {
            return Ok(await _reviewRepository.GetAllByTourId(id));
        }

        
        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _reviewRepository.GetById(id));
        }


        [HttpPost]
        public async Task<ActionResult> ChangeReviewStatus([FromBody] ReviewStatusChange reviewStatusChange)
        {
            var reviewToChange = await _reviewRepository.GetById(reviewStatusChange.ReviewId);
            reviewToChange.Status = reviewStatusChange.NewStatus;
            await _reviewRepository.Update(reviewToChange);
            return Ok();
        }

        
        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] Review review)
        {
            return Ok(await _reviewRepository.Insert(review));
        }

        // PUT: api/Review/5
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Review review)
        {
            await _reviewRepository.Update(review);
            return Ok();
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _reviewRepository.Delete(id);
            return Ok();
        }
    }
}
