using ActivityMe.Common.Models.Entities;
using ActivityMe.Groups.API.Clients;
using ActivityMe.Groups.API.Models;
using ActivityMe.Groups.API.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Play.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ActivityMe.Groups.API.Models.Contracts.GroupDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ActivityMe.Groups.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IRepository<Group> _repository;
        private readonly UserClient userClient;

        public GroupsController(IRepository<Group> repository, UserClient userClient)
        {
            _repository = repository;
            this.userClient = userClient;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var group = await _repository.GetAsync(id);
            if (group == null) return NotFound();

            //var memebers = group.Members.Select(m=> new GroupMember {
            //    FirstName = m.FirstName,
            //    IsActive = m.IsActive,
            //    LastName = m.LastName,
            //    Phone = m.Phone,
            //    UserId = m.UserId
            //}).ToList();
            var dto = new GroupGetDto(group.Id, group.Name, group.Category, group.IsActive, group.Annoucement, group.HostUserId, group.Country, group.City, group.Members);
            return Ok(dto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserGroups()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(s => s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            if (userId == null)
            {
                return BadRequest(new { message = "User not found" });
            }


        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Group>> Post([FromBody] GroupCreateDto group)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(s=>s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            

            if (userId == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var accessToken = Request.Headers["Authorization"];
            var user = await userClient.GetUser(new Guid(userId), accessToken);

            if(user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            
            var newGroup = new Group { 
                Name = group.Name,
                Category = group.Category,
                DateCreated = DateTime.UtcNow,
                HostUserId = user.Id,
                City = group.City,
                Country = group.Country,
                IsActive = true
            };

            await _repository.CreateAsync(newGroup);

            return CreatedAtAction("Get", new { id = newGroup.Id }, newGroup);
        }

        [HttpPost]
        [Route("{groupId}/members")]
        [Authorize]
        public async Task<IActionResult> CreateMemeber(Guid groupId, [FromBody] MemberCreateDto member)
        {
            var userId = member.UserId;
            var accessToken = Request.Headers["Authorization"];
            var user = await userClient.GetUser(userId, accessToken);

            if(user == null)
            {
                return NotFound("user not found");
            }

            var group = await _repository.GetAsync(groupId);
            if (group == null) return NotFound();

            var newGroup = group;
            if(newGroup.Members == null)
            {
                newGroup.Members = new List<GroupMember>();
            }

            //TODO check if user has been added, if added already, should it been still replaced

            newGroup.Members.Add(new GroupMember {
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = true,
                Phone = user.Phone,
                RelevantExperience = user.PlayerExperience.FirstOrDefault(x=>x.Key == group.Category).Value
            });

            await _repository.UpdateAsync(newGroup);

            return Ok(newGroup);
        }
    }
}
