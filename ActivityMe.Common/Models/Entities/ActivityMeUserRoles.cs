using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace ActivityMe.Common.Models.Entities
{
    [CollectionName("UserRoles")]
    public class ActivityMeUserRoles : MongoIdentityRole<Guid>
    {   
    }
}
