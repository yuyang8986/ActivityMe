using System;
using System.Collections.Generic;
using Play.Common;

namespace ActivityMe.Common.Models.Entities.Groups
{
    public class Group : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public GroupCategory Category { get; set; }

        public bool IsActive { get; set; } = true;
        public string Annoucement { get; set; }
        public Guid HostUserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public ICollection<GroupMember> Members { get; set; }
    }
}
