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
        public static Product PrepareProduct()
        {
            return new Product();
        }
        public static Product PrepareProduct(string productSKU)
        {
            return new Product(productSKU);
        }
        public static ProductResponse PrepareEmptyProductResponse() 
        {
            return new ProductResponse(); 
        }
        internal static Rate PrepareEmptyRate()
        {
            return new Rate();
        }

        public static ProductResponse PrepareProductResponse(Product productFinded)
        {
            ProductResponse product = new ProductResponse();
            product.currency = CurrencyConstants.EUR;
            product.sum = productFinded.transactions.Sum(x => x.amount); //REFACTOR
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
            var resultList = PrepareListTransactionResponse();

            foreach (var item in listToConvert)
            {
                var transaction = PrepareTransactionResponse(item);
                resultList.Add(transaction);
            };

            return resultList;

        }

        public static IEnumerable<ProductOnlyResponse> PrepareListProductOnlyResponse(IEnumerable<Product> listToConvert)
        {
            var resultList = new List<ProductOnlyResponse>();
            foreach (var item in listToConvert)
            {
                var productOnly = PrepareProductOnlyResponse(item);
                resultList.Add(productOnly);
            };
            return resultList;
        }

        private static ProductOnlyResponse PrepareProductOnlyResponse(Product item)
        {
            return new ProductOnlyResponse(item);
        }

        public static Product PrepareEmptyProduct()
        {
            return new Product();
        }

        public static IEnumerable<ProductOnlyResponse> PrepareEmptyListProductOnlyResponse()
        {
            return new List<ProductOnlyResponse>();
        }

        public static IEnumerable<ProductResponse> PrepareListProductResponse(IEnumerable<Product> listToConvert)
        {
            var resultList = PrepareEmptyListProductResponse();
            foreach (var item in listToConvert)
            {
                resultList.Add(PrepareProductResponse(item));
            }
            return resultList;
        }

        public static List<ProductResponse> PrepareEmptyListProductResponse()
        {
            return new List<ProductResponse>();
        }

        public static TransactionResponse PrepareTransactionResponse(Transaction item)
        {
            return new TransactionResponse(item);
        }

        public static List<TransactionResponse> PrepareListTransactionResponse()
        {
            return new List<TransactionResponse>();
        }

        public static Transaction PrepareTransaction()
        {
            return new Transaction();
        }

        public static List<RateResponse> PrepareListOfRates(IEnumerable<Rate> listToConvert)
        {
            var resultList = PrepareListOfRates();
            foreach (var item in listToConvert)
            {
                var rate = PrepareRateResponse(item);
                resultList.Add(rate);
            };
            return resultList;

        }

        public static RateResponse PrepareRateResponse(Rate item)
        {
            return PrepareRate(item);
        }

        public static RateResponse PrepareRate(Rate item)
        {
            return new RateResponse(item);
        }

        public static List<RateResponse> PrepareListOfRates()
        {
            return new List<RateResponse>();
        }
    }
}
