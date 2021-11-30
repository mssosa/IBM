using IBM.Application.Interfaces;
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
        private readonly IComunicationRepository comunication;
        private readonly ILogger<TransactionService> log;
        private readonly IConfiguration configuration;
        private readonly ITransactionRepository repository;

        public TransactionService(IComunicationRepository comunication, ILogger<TransactionService> log, IConfiguration configuration, ITransactionRepository repository)
        {
            this.comunication = comunication;
            this.log = log;
            this.configuration = configuration;
            this.repository = repository;
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            var uri = configuration.GetSection("HerokuAPI_TransacitionService").Value;

            log.LogInformation("Iniciando comunicación");
            var response = await comunication.ReadExternalApiAsync(uri);
            IEnumerable<Transaction> result = await ReadResponse(response);
            
            if(result.Any())
            {
                log.LogDebug("Almacenando en la base de datos");
                await repository.AddRangeAsync(result);
                log.LogDebug("Almacenando finalizado");
            }
                
            return result;
        }
        //TODO MEJORAR ESTO
        public async Task<IEnumerable<Transaction>> ReadResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(json);
            return result;
        }
    }

}