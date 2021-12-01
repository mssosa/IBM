using IBM.Core.Entities;
using System.Collections.Generic;

namespace IBM.Core.DTO
{
    public class ProductResponse
    {
        public ProductResponse()
        {
            total = 0;
            sum = 0;
            transactions = new List<Transaction>();
        }
        public List<Transaction> transactions { get; set; }
        public decimal sum { get; set; }
        public int total { get; set; }
        public string currency { get; set; }
    }
}
