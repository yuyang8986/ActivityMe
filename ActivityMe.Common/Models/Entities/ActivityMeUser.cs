using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace ActivityMe.Common.Models.Entities
{
    [CollectionName("Users")]
    public class ActivityMeUser : MongoIdentityUser<Guid>
    {
    }
}
