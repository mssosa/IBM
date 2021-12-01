using IBM.Application.Interfaces;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Application.Services
{
    public class RateService : IRateService, IReadResponse<Rate>
    {
        private readonly IExternalProviderRepository comunication;
        private readonly ILogger<RateService> log;
        private readonly IConfiguration configuration;
        private readonly IRateRepository repository;

        public RateService(IExternalProviderRepository comunication, ILogger<RateService> log, IConfiguration configuration, IRateRepository repository)
        {
            this.comunication = comunication;
            this.log = log;
            this.configuration = configuration;
            this.repository = repository;
        }
        public async Task<IEnumerable<Rate>> GetRatesAsync()
        {
            var uri = configuration.GetSection("HerokuAPI_RatesService").Value;
            log.LogInformation("Iniciando comunicación");
            var response = await comunication.ReadExternalApiAsync(uri);
            IEnumerable<Rate> result = await ReadResponse(response);

            if (result.Any())
            {
                log.LogDebug("Almacenando en la base de datos");
                await repository.AddRangeAsync(result);
                log.LogDebug("Almacenando finalizado");
            }

            return result;
        }
        //TODO MEJORAR ESTO
        public async Task<IEnumerable<Rate>> ReadResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Rate>>(json);
            return result;
        }
    }

}
