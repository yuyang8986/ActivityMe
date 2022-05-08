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
        public async Task<IActionResult> Get(Guid id)
        {
            var group = await _repository.GetAsync(id);
            return Ok(group);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Group>> Post([FromBody] GroupCreateDto group)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(s=>s.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;

            if(userId == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var user = await userClient.GetUser(new Guid(userId));

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
    }
}
