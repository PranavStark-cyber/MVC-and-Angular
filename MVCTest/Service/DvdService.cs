using MVCTest.Models;
using Newtonsoft.Json;

namespace MVCTest.Service
{
    public class DvdService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public DvdService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"];
        }

        public async Task<List<Dvd>> GetAllDvdsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}Manager/GetAllDvds");
                return JsonConvert.DeserializeObject<List<Dvd>>(response);
            }
            catch (Exception ex)
            {
                // Handle error (log, throw custom exception, etc.)
                throw new Exception("Error fetching DVD data", ex);
            }
        }

    }
}
