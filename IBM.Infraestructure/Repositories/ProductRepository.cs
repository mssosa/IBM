using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IBMContext context;
        public ProductRepository(IBMContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Product> GetBySKUAsync(string sku)
        {
            return await context.Products
                .Include(r=>r.transactions)
                .Where(x=>x.sku.Equals(sku))
                .FirstOrDefaultAsync();
        }
      
    }
}
