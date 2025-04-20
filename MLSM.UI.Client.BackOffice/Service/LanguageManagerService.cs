using MLSM.UI.Client.BackOffice.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MLSM.UI.Client.BackOffice.Service
{
    public class LanguageManagerService
    {
        private string _apiUrl;

        public LanguageManagerService(string apiUrl)
        {
            _apiUrl = apiUrl;
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

        public async Task<List<LanguageStringResponseDto>> GetStrings(LangStringFilter filter)
        {
            var result = new List<LanguageStringResponseDto>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var queryString = $"?DateTimeStamp={filter.DateTimeStamp}";
                HttpResponseMessage response = await client.GetAsync($"api/strings{queryString}");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<List<LanguageStringResponseDto>>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                return result ?? new List<LanguageStringResponseDto>();
            }
        }

        public async Task<string> GetLastModified()
        {
            string result = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/strings");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<string>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? "";
                }
                return result ?? "";
            }
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
                HttpResponseMessage response;
                if (requestDto.Action == 'C')
                {
                    response = await client.PostAsync("api/strings", httpContent);
                }
                else if (requestDto.Action == 'U')
                {
                    response = await client.PutAsync("api/strings/" + requestDto.OldCode, httpContent);

                }
                else
                {
                    response = await client.DeleteAsync("api/strings/" + requestDto.Code);

                }

                string res = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(res))
                {
                    result = JsonSerializer.Deserialize<GenericResponseApi>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                return result ?? new GenericResponseApi { ResponseCode = "500", ResponseMessage = "Internal server error" };
            }
        }
    }
}
