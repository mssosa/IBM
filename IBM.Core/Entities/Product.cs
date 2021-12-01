using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.Entities
{
    public class Product : EntityBase
    {

        public Product()
        {

        }
        public Product(string productSKU)
        {
            sku = productSKU;
        }

        public string sku { get; set; }

        public IEnumerable<Transaction> transactions { get; set; } = new List<Transaction>();
    }
}
