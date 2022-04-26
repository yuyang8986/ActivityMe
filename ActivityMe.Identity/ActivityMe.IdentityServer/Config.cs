// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace ActivityMe.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> ApiResources =>
           new ApiResource[]
           {
                new ApiResource()
                {

                }
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                
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
                    AllowedScopes = new List<string> {"All"}
                }
            };
    }
}