using IBM.Core.DTO;
using IBM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponse>> GetTransactionsAsync();
        Task ClearData();
    }
}
