using IBM.Core.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Core.Interfaces
{
    public interface IComunicationRepository
    {
        Task<HttpResponseMessage> ReadExternalApiAsync(string urlParameter);
    }
}
