using IBM.Application.Interfaces;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
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

        public TransactionService(IComunicationRepository comunication, ILogger<TransactionService> log)
        {
            this.comunication = comunication;
            this.log = log;
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            log.LogInformation("Iniciando comunicación");
            var response = await comunication.ReadExternalApiAsync(Resources.PARAMETRO_URL_TRANSACTIONS);
            IEnumerable<Transaction> result = await ReadResponse(response);
            
            if(result.Any())
            {
                //TODO --> ELIMINAR Y PERSISTIR
            }
                
            return result;
        }

        public async Task<IEnumerable<Transaction>> ReadResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(json);
            return result;
        }
    }

}
