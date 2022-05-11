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

        public async Task<GetUserDto> GetUser(Guid userId, string token)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var user = await _httpClient.GetFromJsonAsync<GetUserDto>($"/api/users/{userId}");
            return user;
        }

        public async Task AddGroupToUser(Guid userId, string token, AddGroupToUserDto dto)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            await _httpClient.PostAsJsonAsync($"/api/users/{userId}/groups", dto);
        }
    }
}
