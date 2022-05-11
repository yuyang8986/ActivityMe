using ActivityMe.Common.Models.Entities;
using System;
using System.Collections.Generic;
using ActivityMe.Common.Models.Entities.Groups;

namespace ActivityMe.Groups.API.Models.Contracts
{
    public class GroupCreateDto
    {     
        public string Name { get; set; }    
        public GroupCategory Category { get; set; }    
      
        //public Guid HostUserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
