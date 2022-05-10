using ActivityMe.Common.Models.Entities;
using ActivityMe.Users.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static ActivityMe.Users.API.Models.UserDtos;

namespace ActivityMe.Users.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ActivityMeUser> _userManager;

        public UsersController(UserManager<ActivityMeUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser(UserCreateDto userCreateDto)
        {
            ActivityMeUser user = new ActivityMeUser
            {
                UserName = userCreateDto.Email,
                Email = userCreateDto.Email,
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                PhoneNumber = userCreateDto.Phone,
                PlayerExperience = userCreateDto.PlayerExperience
                
            };

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetUser), new {userId = user.Id}, user);
            }

            return BadRequest(result);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userDto = new GetUserDto(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.PlayerExperience);
            return Ok(userDto);
        }
    }
}
