using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using static ActivityMe.Groups.API.Models.Contracts.UserDtos;

namespace ActivityMe.Groups.API.Clients
{
    public class UserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetUserDto> GetUser(Guid userId)
        {
            var user = await _httpClient.GetFromJsonAsync<GetUserDto>($"/api/users/{userId}");
            return user;
        }
    }
}
