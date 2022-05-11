using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ActivityMe.Common.Models.Entities.Users
{
    [CollectionName("UserRoles")]
    public class ActivityMeUserRoles : MongoIdentityRole<Guid>
    {   
    }
}
