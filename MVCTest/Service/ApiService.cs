using MVCTest.Endpoints;
using static System.Net.Mime.MediaTypeNames;

namespace MVCTest.Service
{
    public class ApiService
    {
        private readonly HttpService _httpService;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpService httpService, ILogger<ApiService> logger)
        {
            _httpService = httpService;
            _logger = logger;
        }

        public async Task<ResponseDto<List<Products>>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpService.GetAsync<List<Products>>(ProductEndpoints.GetAll);

                if (response.IsError)
                {
                    _logger.LogError("Failed to fetch all products: {Error}", response.Error);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all products.");
                return new ResponseDto<List<Products>>
                {
                    IsError = true,

                };
            }
        }
    }
    public class Products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }

    }
}
