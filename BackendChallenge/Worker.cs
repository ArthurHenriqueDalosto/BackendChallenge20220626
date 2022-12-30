using BackendChallenge.Services;
using ChallangeData.DataContext;
using ChallangeData.Model;
using HtmlAgilityPack;
using Nest;
using System.Data;

namespace BackendChallenge
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void StartScrapping()
        {
            List<string> links = GetLinks("https://world.openfoodfacts.org/");
            List<Product> products = GetProductDetails(links);

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IProductRepository scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IProductRepository>();


                scopedProcessingService.Add(products);
                scopedProcessingService.Commit();
            }

        }
        static HtmlDocument GetDocument(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(url);
                return document;
            }
            catch (Exception)
            {

                throw;
            }
        }
        static List<Product> GetProductDetails(List<string> urls)
        {
            try
            {

                List<Product> products = new List<Product>();
                foreach (var item in urls)
                {
                    Product product = new Product();
                    HtmlDocument document = GetDocument(item);

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
                    products.Add(product);
                }
                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }
        static List<string> GetLinks(string url)
        {
            try
            {
                List<string> links = new List<string>();
                HtmlDocument doc = GetDocument(url);
                HtmlNodeCollection linkCollection = doc.DocumentNode.SelectNodes("//div/div/div/div/ul/li/*");

                Uri baseUri = new Uri(url);

                foreach (var item in linkCollection)
                {
                    if (item.LinePosition == 10)
                    {
                        string href = item.Attributes["href"].Value;
                        links.Add(new Uri(baseUri, relativeUri: href).AbsoluteUri);
                    }
                }
                return links;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StartScrapping();
            throw new();
        }
    }
}
