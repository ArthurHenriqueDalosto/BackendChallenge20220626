using BackendChallenge.Repository;
using BackendChallenge.Services;
using ChallangeData.Helper.Enum;
using ChallangeData.Model.Product;
using HtmlAgilityPack;
using System;

namespace BackendChallenge
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (DateTime.UtcNow.Hour == _configuration.GetValue<DateTime>("SchedulingOfScrapping").Hour
                       && DateTime.UtcNow.Minute == _configuration.GetValue<DateTime>("SchedulingOfScrapping").Minute)
                {
                    await DoWorkAsync();
                    await Task.Delay(60000);
                }
                await Task.Delay(1000, stoppingToken);
                Task.CompletedTask.Dispose();
                await Task.Factory.StartNew(async () => { await ExecuteAsync(stoppingToken); });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException);
            }
        }
        private async Task DoWorkAsync()
        {
            try
            {
                List<string> links = GetLinks("https://world.openfoodfacts.org/").Result;
                List<Product> products = GetProductDetails(links).Result;

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IProductRepository scopedProcessingService =
                        scope.ServiceProvider.GetRequiredService<IProductRepository>();


                    scopedProcessingService.Add(products);
                    scopedProcessingService.Commit();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException);
            }
            Task.CompletedTask.Wait();
        }
        private Task<List<string>> GetLinks(string url)
        {
            List<string> links = new List<string>();
            try
            {
                HtmlDocument doc = GetDocument(url).Result;
                HtmlNodeCollection linkCollection = doc.DocumentNode.SelectNodes("//div/div/div/div/ul/li/*");

                Uri baseUri = new Uri(url);

                foreach (var item in linkCollection)
                {
                    if (links.Count < 100 && item.LinePosition == 10)
                    {
                        string href = item.Attributes["href"].Value;
                        links.Add(new Uri(baseUri, relativeUri: href).AbsoluteUri);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException);
            }
            Task.CompletedTask.Dispose();
            return Task.FromResult(links);
        }
        private Task<HtmlDocument> GetDocument(string url)
        {
            HtmlDocument document = new();
            try
            {
                HtmlWeb web = new HtmlWeb();
                document = web.Load(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException);
            }
            Task.CompletedTask.Dispose();
            return Task.FromResult(document);
        }
        private Task<List<Product>> GetProductDetails(List<string> urls)
        {
            List<Product> products = new List<Product>();
            try
            {
                foreach (var item in urls)
                {
                    Product product = new Product();
                    HtmlDocument document = GetDocument(item).Result;

                    product.url = item;
                    product.code = long.Parse(document.DocumentNode.SelectSingleNode("//*[@id=\"barcode\"]").InnerText);
                    product.barcode = document.DocumentNode.SelectSingleNode("//*[@id=\"barcode_paragraph\"]").InnerText.Replace("/n", "").Replace("Barcode:", "").Trim();

                    if (document.DocumentNode.SelectSingleNode("//*[@id=\"og_image\"]") != null)
                        product.image_url = document.DocumentNode.SelectSingleNode("//*[@id=\"og_image\"]").Attributes["src"].Value;
                    else
                        product.image_url = "Image is not available from the source!";

                    if (document.DocumentNode.SelectSingleNode("//*[@id=\"product\"]/div/div/div[2]/div/div[2]/h2") != null)
                        product.product_name = document.DocumentNode.SelectSingleNode("//*[@id=\"product\"]/div/div/div[2]/div/div[2]/h2").FirstChild.InnerText;
                    else
                        product.product_name = document.DocumentNode.SelectSingleNode("//*[@id=\"product\"]/div/div/div[2]/h2").FirstChild.InnerText;

                    if (document.DocumentNode.SelectSingleNode("//*[@id=\"field_quantity_value\"]") != null)
                        product.quantity = document.DocumentNode.SelectSingleNode("//*[@id=\"field_quantity_value\"]").InnerText;
                    else
                        product.quantity = "Quantity data is not available from the source!";

                    if (document.DocumentNode.SelectSingleNode("//*[@id=\"field_categories_value\"]") != null)
                        product.categories = document.DocumentNode.SelectSingleNode("//*[@id=\"field_categories_value\"]").InnerText;
                    else
                        product.categories = "Categories data is not available from the source!";

                    if (document.DocumentNode.SelectSingleNode("//*[@id=\"field_packaging_value\"]") != null)
                        product.packaging = document.DocumentNode.SelectSingleNode("//*[@id=\"field_packaging_value\"]").InnerText;
                    else
                        product.packaging = "Packaging data is not available from the source!";

                    if (document.DocumentNode.SelectSingleNode("//*[@id=\"field_brands_value\"]") != null)
                        product.brands = document.DocumentNode.SelectSingleNode("//*[@id=\"field_brands_value\"]").InnerText;
                    else
                        product.brands = "Brands data is not available from the source!";

                    product.imported_t = DateTime.UtcNow;
                    product.status = Status.Imported;
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " " + ex.InnerException);
            }
            Task.CompletedTask.Dispose();
            return Task.FromResult(products);
        }
    }
}
