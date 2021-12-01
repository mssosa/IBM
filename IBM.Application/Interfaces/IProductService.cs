using IBM.Core.DTO;
using IBM.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBM.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductOnlyResponse>> GetProductsAsync();
        Task<ProductResponse> GetProductAsync(ProductRequest request);
        Task RecordTransactionsForEachProductAsnyc(IEnumerable<Transaction> result);
        Task ClearTransactionData();
    }
}
