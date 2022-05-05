using ActivityMe.Groups.API.Models;
using ActivityMe.Groups.API.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public GroupsController(IRepository<Group> repository)
        {
            _repository = repository;
        }


        // GET api/<GroupsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var group = await _repository.GetAsync(id);
            return Ok(group);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Group>> Post([FromBody] GroupCreateDto group)
        {
            //TODO add checking on HostUserId is exist
            var newGroup = new Group { 
                Name = group.Name,
                Category = group.Category,
                DateCreated = DateTime.UtcNow,
                HostUserId = group.HostUserId,
                City = group.City,
                Country = group.Country,
                IsActive = true
            };

            await _repository.CreateAsync(newGroup);

            return CreatedAtAction("Get", new { id = newGroup.Id }, newGroup);
        }
    }
}
