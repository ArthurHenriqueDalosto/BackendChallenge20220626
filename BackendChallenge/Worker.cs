using ChallangeData.Model;
using HtmlAgilityPack;
using System.Data;

namespace BackendChallenge
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public void StartScrapping()
        {
            List<string> links = GetLinks("https://world.openfoodfacts.org/");
            List<Product> products = GetProductDetails(links);
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
            List<Product> products = new List<Product>();
            foreach (var item in urls)
            {
                HtmlDocument document = GetDocument(item);
                var barcodeXpath = "//*[@id=\"product\"]/div/div/div[2]/div/div[2]/h2";
                Product product = new Product();
                product.barcode = document.DocumentNode.SelectSingleNode(barcodeXpath).InnerText;
                products.Add(product);
            }
            return products;
        }

        static List<string> GetLinks(string url)
        {
            try
            {
                List<string> links = new List<string>();
                HtmlDocument doc = GetDocument(url);
                HtmlNodeCollection linkCollection = doc.DocumentNode.SelectNodes("//../ul/li");
                Uri baseUri = new Uri(url);

                foreach (var item in linkCollection)
                {
                    string href = item.Attributes["href"].Value;
                    links.Add(new Uri(baseUri, relativeUri: href).AbsoluteUri);
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
            throw new NotImplementedException();
        }
    }
}
