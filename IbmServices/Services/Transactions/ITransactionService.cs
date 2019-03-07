using IbmServices.Models;
using System.Collections.Generic;

namespace IbmServices.Services.Transactions
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();

        IEnumerable<Transaction> GetMany(string Sku);
    }
}
