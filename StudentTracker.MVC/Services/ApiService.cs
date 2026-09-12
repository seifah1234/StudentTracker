using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace StudentTracker.MVC.Services
{
    /// <summary>
    /// طبقة اتصال عامة بين الـ MVC frontend وبين StudentTracker.PL (Web API).
    /// تقوم تلقائياً بإرفاق الـ JWT (لو المستخدم مسجّل دخول) في كل طلب.
    /// </summary>
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            AttachToken();
        }

        private void AttachToken()
        {
            var token = _httpContextAccessor.HttpContext?.User?.FindFirst("access_token")?.Value;
            _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token)
                ? null
                : new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        public async Task<List<T>> GetListAsync<T>(string endpoint)
        {
            var result = await GetAsync<List<T>>(endpoint);
            return result ?? new List<T>();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
            => await _httpClient.PostAsJsonAsync(endpoint, data, JsonOptions);

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
            => await _httpClient.PutAsJsonAsync(endpoint, data, JsonOptions);

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
            => await _httpClient.DeleteAsync(endpoint);

        public async Task<bool> ExistsAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return false;
            var json = await response.Content.ReadAsStringAsync();
            return bool.TryParse(json, out var exists) && exists;
        }
    }
}
