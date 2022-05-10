using ActivityMe.Common.Models.Entities;
using System;
using System.Collections.Generic;

namespace ActivityMe.Users.API.Models
{
    public class UserDtos
    {
        public record GetUserDto(Guid Id, string FirstName, string LastName, string Email, string Phone, Dictionary<GroupCategory, int> PlayerExperience);
    }
}
