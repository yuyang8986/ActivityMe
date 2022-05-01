// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace ActivityMe.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "roles", new List<string>{JwtClaimTypes.Role})
            };

        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "Larry",
                    Password = "Pass123$",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Larry"),
                        new Claim(JwtClaimTypes.Email, "Larry@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Email),
                        //new Claim(JwtClaimTypes.Address, @"{'street_address:'123 St}"),


                    }
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
           new ApiResource[]
           {
                new ApiResource()
                {
                    Name = "GroupApi",
                    DisplayName = "GroupApi",
                    Scopes = new List<string>
                    {
                        "Group:All"
                    }
                }
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope()
                {
                    Name = "Group:All"
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client()
                {
                    ClientId = "ABC",
                    ClientName = "ActivityMeMobile",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("Secret".Sha256()) },
                    AllowedScopes = new List<string> {"Group:All"}
                },

                new Client ()
                {
                    ClientId = "ActivityMe-code",
                    ClientName = "ActivityMe-App",
                    AllowedGrantTypes= GrantTypes.Code,
                    RedirectUris =
                    {
                        "https://localhost:5001/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5001/signout-callback-oidc"
                    },
                    ClientSecrets = {new Secret("Secret".Sha256())},
                    AllowedScopes = new List<string> {"Group:All"},
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = true,

                }
            };
    }
}