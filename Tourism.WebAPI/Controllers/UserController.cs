using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Domain.AdditionalModels;
using Tourism.Domain.Models;
using Tourism.Infrastructure.Interfaces;

namespace Tourism.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAll());
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _userRepository.GetById(id));
        }


        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegister user)
        {
            if (await _userRepository.GetByEmail(user.Email) != null)
            {
                return BadRequest("User Already Exist");
            }

            return Ok(await _userRepository.Insert(new User()
            {
                Email = user.Email,
                CreatedAt = DateTime.Now,
                Name = user.Name,
                Password = user.Password,
                Role = user.Role,
                UserId = new Guid()
            }));
        }

        
        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLogin login)
        {
            var user  = await _userRepository.GetByEmail(login.Email);
            if (user == null || user.Password != login.Password)
            {
                return BadRequest("Wrong email or password");
            }
            return Ok(user.UserId);
        }

        
        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] User user)
        {
            return Ok(await _userRepository.Insert(user));
        }

        // POST: api/User/5
        [HttpPost]
        public async Task<ActionResult> Update([FromBody] User user)
        {
            await _userRepository.Update(user);
            return Ok();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userRepository.Delete(id);
            return Ok();
        }
    }
}