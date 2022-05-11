using System;
using ActivityMe.Common.Models.Entities.Groups;

namespace ActivityMe.Common.Models.Entities.Users
{
    public class UserGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GroupCategory Category { get; set; }
        public Guid HostUserId { get; set; }
        public string HostUserName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}