using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Infrastructure.Repositories
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {
        private readonly IBMContext context;

        public RateRepository(IBMContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Rate> GetBySKUAsync(string sku)
        {
            return await context.Set<Rate>().FindAsync(sku);
        }
    }
}
