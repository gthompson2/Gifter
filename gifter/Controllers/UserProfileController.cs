using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gifter.Repositories;
using Gifter.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gifter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userProfileRepository.GetAll());
        }

        // https://localhost:5001/api/UserProfile/
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var profile = _userProfileRepository.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpGet("GetProfileByIdWithPosts/{id}")] 
        public IActionResult GetProfileByIdWithPosts(int id)
        {
            var profile = _userProfileRepository.GetProfileByIdWithPosts(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpPost]
        public IActionResult Post(UserProfile profile)
        {
            _userProfileRepository.Add(profile);
            return CreatedAtAction("Get", new { id = profile.Id }, profile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(profile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
    }
}
