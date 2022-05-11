using ActivityMe.Common.Models.Entities;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using ActivityMe.Common.Models.Entities.Users;

namespace ActivityMe.IdentityServer
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ActivityMeUser> userManager;

        public CustomResourceOwnerPasswordValidator(UserManager<ActivityMeUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (string.IsNullOrEmpty(context.UserName)) throw new ArgumentNullException("User is null");

            var user = await userManager.FindByNameAsync(context.UserName);

            if (user == null) throw new ArgumentNullException("User is null");
            var ok =  await userManager.CheckPasswordAsync(user, context.Password);
            if(ok)
            {
                context.Result = new GrantValidationResult(subject: user.Id.ToString(), authenticationMethod: "password");
            }

            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid password");
            }
        }      
    }
}
