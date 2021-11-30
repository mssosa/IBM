using IBM.Core.Entities;
using IBM.Core.Interfaces;
using IBM.Infrastructure.Data;

namespace IBM.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly IBMContext context;

        public TransactionRepository(IBMContext context) : base(context)
        {
            this.context = context;
        }

    }
}
