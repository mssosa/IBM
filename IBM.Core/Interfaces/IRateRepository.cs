using IBM.Core.Entities;
using System.Threading.Tasks;

namespace IBM.Core.Interfaces
{
    public interface IRateRepository : IGenericRepository<Rate>
    {
        Task<Rate> GetBySKUAsync(string sku);
    }
}
