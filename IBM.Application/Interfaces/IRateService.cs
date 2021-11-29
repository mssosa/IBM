using IBM.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBM.Application.Interfaces
{
    public interface IRateService
    {
        Task<IEnumerable<Rate>> GetRatesAsync();
    }
}
