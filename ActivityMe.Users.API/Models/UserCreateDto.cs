using ActivityMe.Common.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ActivityMe.Common.Models.Entities.Groups;

namespace ActivityMe.Users.API.Models
{
    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

        public Dictionary<GroupCategory, int> PlayerExperience { get; set; }
    }
}
