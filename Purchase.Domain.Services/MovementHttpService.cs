using Microsoft.AspNetCore.Http;
using Purchase.Api.Model.Models;
using Purchase.Domain.Services.Interfaces;
using System.Net.Http.Json;

namespace Purchase.Domain.Services
{
    public class MovementHttpService : IMovementHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MovementHttpService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterMovementAsync(PurchaseMovementCreatableDto dto)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Replace("Bearer ", ""));
            }
            var response = await _httpClient.PostAsJsonAsync("api/Movement", dto);
            response.EnsureSuccessStatusCode();
        }
    }
}
