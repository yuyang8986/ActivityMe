﻿using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace ActivityMe.Common.Models.Entities
{
    [CollectionName("Users")]
    public class ActivityMeUser : MongoIdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Dictionary<string, string> PlayerExperience { get; set; }
        public IEnumerable<int>  AttendingEventsIds { get; set; }
    }
}
