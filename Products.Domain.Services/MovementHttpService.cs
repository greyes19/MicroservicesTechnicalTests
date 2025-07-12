using Microsoft.AspNetCore.Http;
using Products.Api.Model.Models;
using Products.Domain.Services.Interfaces;
using System.Net.Http.Json;

namespace Products.Domain.Services
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

        public async Task<List<MovementSummaryProductDto>> GetSummaryAsync()
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken.Replace("Bearer ", ""));
            }

            var response = await _httpClient.GetAsync("api/Movement/summary");

            response.EnsureSuccessStatusCode();

            var summary = await response.Content.ReadFromJsonAsync<List<MovementSummaryProductDto>>();

            return summary ?? new List<MovementSummaryProductDto>();
        }
    }
}
