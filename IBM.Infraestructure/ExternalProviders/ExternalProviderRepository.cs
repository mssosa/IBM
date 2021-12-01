using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Infrastructure.ExternalProviders
{
    public class ExternalProviderRepository : IExternalProviderRepository
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ExternalProviderRepository> log;

        public ExternalProviderRepository(HttpClient httpClient, ILogger<ExternalProviderRepository> log)
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
