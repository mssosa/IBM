using IBM.Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Application.Services
{
    public class TransactionServiceBase
    {

        private async Task<IEnumerable<Transaction>> ReadResponse(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(json);
            return result;
        }
    }
}