using IBM.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBM.Application.Interfaces
{
    public interface IRateService
    {
        Task<IEnumerable<RateResponse>> GetRatesAsync(bool offline = false);
    }
}
