using ActivityMe.Common.Models.Entities;
using System;
using System.Collections.Generic;
using ActivityMe.Common.Models.Entities.Groups;

namespace ActivityMe.Groups.API.Models.Contracts
{
    public class UserDtos
    {
        public record AddGroupToUserDto(Guid GroupId, string GroupName, GroupCategory Category, string HostUserId, string HostUserName, string Country, string City, string Location);

        public record GetUserDto(Guid Id, string FirstName, string LastName, string Email, string Phone, Dictionary<string, int> PlayerExperience, ICollection<GroupDtos.GroupGetDto> Groups);
    }
}
