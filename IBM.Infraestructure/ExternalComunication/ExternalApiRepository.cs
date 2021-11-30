using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Infrastructure.ExternalComunication
{
    public class ExternalApiRepository : IComunicationRepository
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ExternalApiRepository> log;

        public ExternalApiRepository(HttpClient httpClient, ILogger<ExternalApiRepository> log)
        {
            this.httpClient = httpClient;
            this.log = log;
        }

        public async Task<HttpResponseMessage> ReadExternalApiAsync(string urlParameter)
        {
            log.LogInformation($"Realizando la transaccion a {httpClient.BaseAddress}/{urlParameter}");
            var result = await httpClient.GetAsync(urlParameter);
            log.LogInformation($"Respuesta =>{result.StatusCode}");
            return result;

        }
      
    }
  
}
