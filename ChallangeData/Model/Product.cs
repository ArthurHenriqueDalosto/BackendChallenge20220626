using ChallangeData.Helper.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallangeData.Model
{
    public class Product
    {
        [Key]
        public int? Id { get; set; }
        public long? code { get; set; }
        public string? barcode { get; set; }
        public Status? status { get; set; }
        public DateTime? imported_t { get; set; }
        public string? url { get; set; }
        public string? product_name { get; set; }
        public string? quantity { get; set; }
        public string? categories { get; set; }
        public string? packaging { get; set; }
        public string? brands { get; set; }
        public string? image_url { get; set; }
        public Product()
        {

        }
    }
}
