using ChallangeData.Model.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallangeData.DataContext
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
        }
        public DataBaseContext(DbContextOptions<DataBaseContext> options):
            base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
        public DbSet<Product> Products { get; set; }
    }
}
