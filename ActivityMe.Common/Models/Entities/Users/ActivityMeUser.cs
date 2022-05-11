using System;
using System.Collections.Generic;
using ActivityMe.Common.Models.Entities.Groups;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using Play.Common;

namespace ActivityMe.Common.Models.Entities.Users
{
    [CollectionName("Users")]
    public class ActivityMeUser : MongoIdentityUser<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        //Player Experience in Years
        public Dictionary<GroupCategory, int> PlayerExperience { get; set; }
        public IEnumerable<int>  AttendingEventsIds { get; set; }
        public ICollection<UserGroup> Groups { get; set; }
    }
}
