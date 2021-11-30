using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddRangeAsync(IEnumerable<TEntity> listToAdd);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task ClearData();

    }
}
