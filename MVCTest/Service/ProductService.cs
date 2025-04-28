namespace MVCTest.Service
{
    public class ProductService
    {
        private readonly ApiService _apiService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApiService apiService, ILogger<ProductService> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<ResponseDto<List<Products>>> GetAllProducts()
        {
            return await _apiService.GetAllProductsAsync();
        }
    }
}
