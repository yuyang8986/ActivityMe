using ActivityMe.Groups.API.Models;
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
        // GET: api/<GroupsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GroupsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GroupsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Group>> Post([FromBody] Group group)
        {
            var newGroup = new Group { 
                Name = group.Name,
                Category = group.Category,
                DateCreated = DateTime.UtcNow,
                HostUserId = group.HostUserId,
                PrimaryLocation = group.PrimaryLocation
            };

            await _repository.CreateAsync(newGroup);

            return CreatedAtAction("Get", new { id = newGroup.Id }, newGroup);
        }

        // PUT api/<GroupsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GroupsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
