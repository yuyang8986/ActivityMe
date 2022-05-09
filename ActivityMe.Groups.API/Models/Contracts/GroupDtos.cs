using ActivityMe.Common.Models.Entities;
using System;
using System.Collections.Generic;

namespace ActivityMe.Groups.API.Models.Contracts
{
    public class GroupDtos
    {
        public record GroupGetDto(Guid Id ,string Name, 
            GroupCategory Category, bool IsActive, string Annoucement, Guid HostUserId, string Country, string City, ICollection<GroupMember> Memebers);

        public record MemberCreateDto(Guid UserId);
    }
}
