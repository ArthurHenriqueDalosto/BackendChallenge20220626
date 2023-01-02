using BackendChallenge.Services;
using ChallangeData.Model.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BackendChallenge.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IConfiguration configuration, ILogger<ProductController> logger, IProductService productService)
        {
            _configuration = configuration;
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("code")]
        public async Task<Product> FindByCode(Int64 code)
        {
            Product response = new Product();
            try
            {
                response = _productService.FindByCode(code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }

        [HttpGet("")]
        public async Task<List<Product>> FindAll()
        {
            List<Product> response = new();
            try
            {
                response = _productService.FindAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
