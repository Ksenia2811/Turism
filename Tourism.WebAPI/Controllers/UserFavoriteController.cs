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
    public class UserFavoriteController : ControllerBase
    {
        private readonly IUserFavoriteRepository _userFavoriteRepository;

        public UserFavoriteController(IUserFavoriteRepository userFavoriteRepository)
        {
            _userFavoriteRepository = userFavoriteRepository;
        }

        // GET: api/UserFavorite
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userFavoriteRepository.GetAll());
        }
        
        
        // GET: api/UserFavorite/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAllByUserId(Guid id)
        {
            return Ok(await _userFavoriteRepository.GetByUserId(id));
        }

        
        // GET: api/UserFavorite/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _userFavoriteRepository.GetById(id));
        }

        // POST: api/UserFavorite
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] UserFavorite userFavorite)
        {
         return Ok(await _userFavoriteRepository.Insert(userFavorite));   
        }

        // PUT: api/UserFavorite/5
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserFavorite userFavorite)
        {
            await _userFavoriteRepository.Update(userFavorite);
            return Ok();
        }

        // DELETE: api/UserFavorite/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userFavoriteRepository.Delete(id);
            return Ok();
        }
    }
}