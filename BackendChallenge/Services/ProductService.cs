using BackendChallenge.Repository;
using ChallangeData.Model.Product;

namespace BackendChallenge.Services
{
    public interface IProductService
    {
        List<Product> FindAll();
        Product FindByCode(Int64 code);
    }
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductService> _logger;
        private IProductRepository productRepository;

        public ProductService(IConfiguration configuration, ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _configuration = configuration;
            _logger = logger;
            this.productRepository = productRepository;
        }



        public Product FindByCode(Int64 code)
        {
            Product response = new();
            try
            {
                var product = productRepository.findByCode(code);

                if (product.Id == null || product == null)
                    return new Product();
                else
                    response = product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }

        public List<Product> FindAll()
        {
            List<Product> response = new();
            try
            {
                var products = productRepository.FindAll();

                if (products.Count() > 0)
                    response = products;
                else
                    return new List<Product>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
