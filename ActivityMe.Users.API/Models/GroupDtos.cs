using System;
using System.Collections.Generic;
using ActivityMe.Common.Models.Entities;
using ActivityMe.Common.Models.Entities.Groups;

namespace ActivityMe.Users.API.Models
{
    public class GroupDtos
    {
        public record GroupGetDto(Guid Id ,string Name, 
            GroupCategory Category, Guid HostUserId, string Country, string City);

        public record MemberCreateDto(Guid UserId);
    }
    
    // public class GroupMember
    // {
    //     public string FirstName { get; set; }
    //     public string LastName { get; set; }
    //     public string UserId { get; set; }
    //     public string Phone { get; set; }
    //     public bool IsActive { get; set; }
    //     public int RelevantExperience { get; set; }
    // }
}