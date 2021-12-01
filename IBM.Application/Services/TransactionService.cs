using IBM.Application.Interfaces;
using IBM.Core.DTO;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Application.Services
{
    public class TransactionService : ITransactionService, IReadResponse<Transaction>
    {
        private readonly IExternalProviderRepository comunication;
        private readonly ILogger<TransactionService> log;
        private readonly IConfiguration configuration;
        private readonly ITransactionRepository repository;
        private readonly IProductService productService;

        public TransactionService(IExternalProviderRepository comunication, ILogger<TransactionService> log, IConfiguration configuration, ITransactionRepository repository, IProductService productService)
        {
            this.comunication = comunication;
            this.log = log;
            this.configuration = configuration;
            this.repository = repository;
            this.productService = productService;
        }

        public async Task ClearData()
        {
            await repository.ClearData();
        }

        public async Task<IEnumerable<TransactionResponse>> GetTransactionsAsync()
        {
            var uri = configuration.GetSection("HerokuAPI_TransacitionService").Value;

            log.LogInformation("Iniciando comunicación");
            var response = await comunication.ReadExternalApiAsync(uri);
            IEnumerable<Transaction> resultFromAPI = await ReadResponse(response);
            
            if(resultFromAPI.Any())
            {
                log.LogDebug("Almacenando en la base de datos");
                await productService.RecordTransactionsForEachProductAsnyc(resultFromAPI);
                log.LogDebug("Almacenando finalizado");
            }

            var returnResponse = Factory.PrepareListTransactionResponse(resultFromAPI);

            return returnResponse;
        }

        public async Task<IEnumerable<Transaction>> ReadResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(json);
            return result;
        }
    }

}