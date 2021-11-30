using IBM.Core.Interfaces;
using IBM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IBMContext context;
        private readonly ILogger log;

        public GenericRepository(IBMContext context)
        {
            this.context = context;
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> listToAdd)
        {
            await ClearData();
            await context.Set<TEntity>().AddRangeAsync(listToAdd);
            await context.SaveChangesAsync();
        }

        public async  Task ClearData()
        {
            var result = await GetAllAsync();
            context.Set<TEntity>().RemoveRange(result);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
    }
}
