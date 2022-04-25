using Play.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityMe.Groups.API.Models
{
    public class Group : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Category { get; set; }
        public Guid HostUserId { get; set; }
        public string PrimaryLocation { get; set; }

    }
}
