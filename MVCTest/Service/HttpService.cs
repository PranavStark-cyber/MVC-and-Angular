using System.Text.Json;

namespace MVCTest.Service
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpService> _logger;

        public HttpService(HttpClient httpClient, IConfiguration configuration, ILogger<HttpService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        private string GetBaseUrl() => _configuration["ApiSettings:BaseUrl"];

        public async Task<ResponseDto<T>> GetAsync<T>(string endpoint)
        {
            var fullUrl = $"{GetBaseUrl()}{endpoint}";
            try
            {
                var response = await _httpClient.GetAsync(fullUrl);
                return await HandleResponse<T>(response);  // Use the generic HandleResponse method
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during GET request to {url}", fullUrl);
                return CreateErrorResponse<T>(ex.Message, ex.GetType().Name);
            }
        }


        private async Task<ResponseDto<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                // Log the raw response content
                _logger.LogInformation("Raw Response Content: {content}", content);

                try
                {
                    // Deserialize into ResponseDto<T>
                    var result = JsonSerializer.Deserialize<ResponseDto<T>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return result ?? CreateErrorResponse<T>("Failed to parse response.", "DeserializationError");
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "JSON deserialization error for content: {content}", content);
                    return CreateErrorResponse<T>("Invalid JSON format.", "JsonException");
                }
            }

            var errorDetails = await response.Content.ReadAsStringAsync();
            return CreateErrorResponse<T>($"Request failed: {response.StatusCode}", errorDetails);
        }



        private ResponseDto<T> CreateErrorResponse<T>(string message, string errorType)
        {
            return new ResponseDto<T>
            {
                IsError = true,

            };
        }
    }

    public class ResponseDto<T>
    {
        public T Result { get; set; }
        public bool IsError { get; set; }
        public string Error { get; set; }
    }

    public class ProductResponse
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<ProductImage> Images { get; set; }
    }

    public class ProductImage
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool IsDefault { get; set; }
        public int SequenceId { get; set; }
    }


    public class ErrorDetails
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public int StatusCode { get; set; }
    }
}
