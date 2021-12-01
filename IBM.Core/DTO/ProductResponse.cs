using IBM.Core.Entities;
using System.Collections.Generic;

namespace IBM.Core.DTO
{
    public class ProductResponse
    {
        public ProductResponse()
        {
            total = 0;
            transactions = new List<Transaction>();
        }
        public List<Transaction> transactions { get; set; }
        public double total { get; set; }
        public string currency { get; set; }
    }
}
