using IBM.Core.DTO;
using IBM.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IBM.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> GetProductBySKUAsync(ProductRequest request);
        Task RecordTransactionsForEachProductAsnyc(IEnumerable<Transaction> result);
        Task ClearTransactionData();
    }
}
