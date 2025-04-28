namespace MVCTest.Endpoints
{
    public class ProductEndpoints
    {
        private const string Base = "api/products";

        public const string GetAll = $"{Base}";
        public const string GetById = $"{Base}/{{id}}";  // Endpoint for fetching a product by ID
        public static string GetPaginated(int pageNumber, int pageSize) =>
            $"{Base}?pageNumber={pageNumber}&pageSize={pageSize}";
    }
}
