using ChallangeData.DataContext;
using ChallangeData.Model.Product;

namespace BackendChallenge.Repository
{
    public interface IProductRepository
    {
        void Add(List<Product> products);
        Product findByCode(long id);
        void Commit();
        List<Product> FindAll();
    }
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext db;

        public ProductRepository(DataBaseContext db)
        {
            this.db = db;
        }

        public void Add(List<Product> products)
        {
            db.Products.AddRange(products);
        }

        public Product findByCode(long code)
        {
            var product = db.Products.Where(x => x.code == code).FirstOrDefault();
            return product;
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public List<Product> FindAll()
        {
            var products = db.Products.ToList();
            return products;
        }
    }
}
