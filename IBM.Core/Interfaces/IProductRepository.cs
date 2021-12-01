using IBM.Core.Entities;
using System.Threading.Tasks;

namespace IBM.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetBySKUAsync(string sku);
    }
}
