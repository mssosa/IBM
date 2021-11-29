using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IBM.Application.Interfaces
{
    public interface IReadResponse<T> where T : class
    {
        Task<IEnumerable<T>> ReadResponse(HttpResponseMessage response);

    }
}
