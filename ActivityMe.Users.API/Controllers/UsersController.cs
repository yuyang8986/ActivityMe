using ActivityMe.Common.Models.Entities;
using ActivityMe.Users.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityMe.Common.Models.Entities.Groups;
using ActivityMe.Common.Models.Entities.Users;
using Play.Common;
using static ActivityMe.Users.API.Models.UserDtos;

namespace ActivityMe.Users.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ActivityMeUser> _userManager;
        private readonly IRepository<ActivityMeUser> _repository;

        public UsersController(UserManager<ActivityMeUser> userManager, IRepository<ActivityMeUser> repository)
        {
            _userManager = userManager;
            _repository = repository;
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

            var groupDtos = user.Groups.Select(g => new GroupDtos.GroupGetDto(g.Id, g.Name, g.Category,
                 g.HostUserId, g.Country, g.City)).ToList();

            var userDto = new GetUserDto(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.PlayerExperience, groupDtos);
            return Ok(userDto);
        }

        [HttpPost("{userId}/groups")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddGroupToUser(string userId, [FromBody] AddGroupToUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.Groups ??= new List<UserGroup>();
            
            user.Groups.Add(new UserGroup()
            {
                Category = dto.Category,
                City = dto.City,
                Country = dto.Country,
                Id = dto.GroupId,
                Name = dto.GroupName,
                HostUserId = new Guid(dto.HostUserId),
                HostUserName = dto.HostUserName
            });

            await _repository.UpdateAsync(user);

            return Ok(user);

        }
    }
}
