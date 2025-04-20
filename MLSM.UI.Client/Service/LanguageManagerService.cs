using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using MLSM.UI.Client.Models;

namespace MLSM.UI.Client.Service
{
    public class LanguageManagerService
    {
        private string _apiUrl;

        public LanguageManagerService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<LanguageStringFiltered> GetLastModified()
        {
            var result = new LanguageStringFiltered();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/lastmodified");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<LanguageStringFiltered>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return result ?? new LanguageStringFiltered();
            }
        }

        public async Task<List<LanguageStringResponseDto>> GetStrings()
        {
            var result = new List<LanguageStringResponseDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/strings");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<List<LanguageStringResponseDto>>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return result ?? new List<LanguageStringResponseDto>();
            }
        }

        public async Task<List<LanguageStringResponseDto>> GetFilteredStrings(string dateTimeStamp)
        {
            var result = new List<LanguageStringResponseDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"api/filteredstrings/{dateTimeStamp}");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<List<LanguageStringResponseDto>>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return result ?? new List<LanguageStringResponseDto>();
            }
        }

    }
}
