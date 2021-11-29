using IBM.Application.Interfaces;
using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Core.ObjectValues;
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
        private readonly IComunicationRepository comunication;
        private readonly ILogger<RateService> log;

        public RateService(IComunicationRepository comunication, ILogger<RateService> log)
        {
            this.comunication = comunication;
            this.log = log;
        }
        public async Task<IEnumerable<Rate>> GetRatesAsync()
        {
            log.LogInformation("Iniciando comunicación");
            var response = await comunication.ReadExternalApiAsync(Resources.PARAMETRO_URL_TRANSACTIONS);
            IEnumerable<Rate> result = await ReadResponse(response);

            if (result.Any())
            {
                //TODO --> ELIMINAR Y PERSISTIR
            }

            return result;
        }

        public async Task<IEnumerable<Rate>> ReadResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Rate>>(json);
            return result;
        }
    }

}
