using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using ChallangeData.DataContext;
using System.Data.Entity.Infrastructure;
using Nest;
using ChallangeData.Model;

namespace BackendChallenge.Services
{
    public interface IProductRepository
    {
        void Add(List<Product> products);
        void findById(string code);
        void Commit();
    }
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext db;

        public ProductRepository(DataContext db)
        {
            this.db = db;
        }

        public void Add(List<Product> products)
        {
            db.Products.AddRange(products);
        }

        public void findById(string code)
        {
            db.Products.Where(x => x.barcode == code).First();
        }

        public void Commit()
        {
            db.SaveChanges();
        }
    }
}
