using IBM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBM.Core.DTO
{

    public class TransactionResponse
    {
        public TransactionResponse(Transaction item)
        {
            sku = item.sku;
            amount = item.amount;
            currency = item.currency;
        }

        public string sku { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }
}
