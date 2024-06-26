﻿using ActivityMe.Common.Models.Entities;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ActivityMe.Common.Models.Entities.Users;

namespace ActivityMe.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ActivityMeUser> _userManager;

        public ProfileService(UserManager<ActivityMeUser> userManager)
        {
            _userManager = userManager;
        }

        async public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

            var user = await _userManager.FindByIdAsync(subjectId);
            if (user == null)
                throw new ArgumentException("Invalid subject identifier");

            var claims = GetClaimsFromUser(user);
            context.IssuedClaims = claims.ToList();
        }

        async public Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

            if(subjectId != null)
            {
                context.IsActive = true;
            }

            else
            {
                context.IsActive = false;
            }
            //var user = await _userManager.FindByIdAsync(subjectId);

            //context.IsActive = false;

            //if (user != null)
            //{
            //    if (_userManager.SupportsUserSecurityStamp)
            //    {
            //        var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
            //        if (security_stamp != null)
            //        {
            //            var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
            //            if (db_security_stamp != security_stamp)
            //                return;
            //        }
            //    }

            //    context.IsActive =
            //        !user.LockoutEnabled ||
            //        !user.LockoutEnd.HasValue ||
            //        user.LockoutEnd <= DateTime.Now;
            //}
        }

        private IEnumerable<Claim> GetClaimsFromUser(ActivityMeUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            claims.Add(new Claim("sid", user.Id.ToString()));
            if (!string.IsNullOrWhiteSpace(user.FirstName))
                claims.Add(new Claim("name", user.FirstName));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                claims.Add(new Claim("last_name", user.LastName));


            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            return claims;
        }
    }
}
