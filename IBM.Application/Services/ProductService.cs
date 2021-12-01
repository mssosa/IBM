using IBM.Application.Interfaces;
using IBM.Core.DTO;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<RateService> log;
        private readonly IProductRepository repository;
        private readonly ITransactionRepository transactionRepository;

        public ProductService(ILogger<RateService> log, IProductRepository repository, ITransactionRepository transactionRepository)
        {
            this.log = log;
            this.repository = repository;
            this.transactionRepository = transactionRepository;
        }
        public async Task<IEnumerable<ProductOnlyResponse>> GetProductsAsync()
        {

            var productFinded = await repository.GetAllAsync();

            if (productFinded == null)
            {
                return Factory.PrepareEmptyListProductOnlyResponse();
            }

            return Factory.PrepareListProductOnlyResponse(productFinded);
        }

       
        public async Task<ProductResponse> GetProductAsync(ProductRequest request)
        {
            if(string.IsNullOrEmpty(request.sku))
                throw new ArgumentNullException(nameof(request.sku));

            var productFinded = await repository.GetBySKUAsync(request.sku);

            if (productFinded == null)
            {
                return Factory.PrepareEmptyProductResponse();
            }

            var productsInEUR = ConvertionCurrencyServices(productFinded);

            return Factory.PrepareProductResponse(productFinded);
        }
        //REFACTOR THIS
        private Product ConvertionCurrencyServices(Product product)
        {
            var resultProduct = Factory.PrepareEmptyProduct();

            var listOfOnlyEur = product.transactions.Where(x=>x.currency.ToUpper().Equals(CurrencyConstants.EUR)).ToList();
            //var listNotEurCurrency = product.transactions.Where(x=>x.currency.ToUpper().Distinct(CurrencyConstants.EUR)).ToList();


            return resultProduct;


        }

        public async Task RecordTransactionsForEachProductAsnyc(IEnumerable<Transaction> listToAdd)
        {
            await ClearTransactionData();
            var listOfProductsToRecord = Factory.PrepareListOfProducts();
            var listOfProducts = listToAdd.Select(m => m.sku).Distinct().ToList();
            foreach (var productSKU in listOfProducts)
            {
                var transactionsToRecord = listToAdd.Where(x => x.sku.Equals(productSKU)).ToList();
                var productToRecord = Factory.PrepareProduct(productSKU);
                productToRecord.transactions = transactionsToRecord;
                listOfProductsToRecord.Add(productToRecord);
            }
            await repository.AddRangeAsync(listOfProductsToRecord);
        }

        public async Task ClearTransactionData()
        {
            await transactionRepository.ClearData();
        }

      
    }
}
