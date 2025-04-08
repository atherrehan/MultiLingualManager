using MLSM.UI.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace MLSM.UI.Service
{
    public class LangManagerService
    {
        private string _apiUrl;
        public LangManagerService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }
        public async Task<GenericResponseApi> Action(LangManagerActionRequestDto requestDto)
        {
            var result = new GenericResponseApi();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string jsonContent = JsonSerializer.Serialize(requestDto);
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(requestDto.Url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<GenericResponseApi>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return result ?? new GenericResponseApi();
            }
        }
    }
}
