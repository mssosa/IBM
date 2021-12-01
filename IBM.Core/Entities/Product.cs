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
            transactions = new List<Transaction>(); 
        }

        public string sku { get; set; }

        public IEnumerable<Transaction> transactions { get; set; }
    }
}
