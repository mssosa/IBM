using IBM.Core.DTO;
using IBM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBM.Core.ObjectValues
{
    public class Factory
    {
        public static Product PrepareProduct() { return new Product(); }
        public static Product PrepareProduct(string productSKU)
        {
            return new Product(productSKU);
        }
        public static ProductResponse PrepareEmptyProductResponse() { return new ProductResponse(); }
        public static ProductResponse PrepareProductResponse(Product productFinded)
        {
            ProductResponse product = new ProductResponse();
            product.currency = CurrencyConstants.EUR;
            product.total = productFinded.transactions.Count();
            product.transactions = productFinded.transactions.ToList();
            return product;
        }

        public static List<Product> PrepareListOfProducts()
        {
            return new List<Product>();
        }

        public static List<TransactionResponse> PrepareListTransactionResponse(IEnumerable<Transaction> listToConvert)
        {
            var resultList= PrepareListTransactionResponse();

            foreach (var item in listToConvert)
            {
                var transaction = PrepareTransactionResponse(item);
                resultList.Add(transaction);
            };

            return resultList;

        }

        public static TransactionResponse PrepareTransactionResponse(Transaction item)
        {
            return new TransactionResponse(item);
        }

        public static List<TransactionResponse> PrepareListTransactionResponse()
        {
            return new List<TransactionResponse>();
        }
    }
}
