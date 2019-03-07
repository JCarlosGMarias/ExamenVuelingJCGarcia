using IbmServices.Models;
using IbmServices.Services.Transactions;
using System.Collections.Generic;
using System.Linq;

namespace IbmTests.Controllers.Mocks
{
    public class TransactionServiceMock : ITransactionService
    {
        public IEnumerable<Transaction> GetAll()
        {
            return new List<Transaction>()
            {
                new Transaction() { Id = 1, Sku = "T2006", Amount = 10.00m, Currency = "USD" },
                new Transaction() { Id = 2, Sku = "M2007", Amount = 34.57m, Currency = "CAD" },
                new Transaction() { Id = 3, Sku = "R2008", Amount = 17.95m, Currency = "USD" },
                new Transaction() { Id = 4, Sku = "T2006", Amount = 7.63m, Currency = "EUR" },
                new Transaction() { Id = 5, Sku = "B2009", Amount = 21.23m, Currency = "USD" }
            };
        }

        public IEnumerable<Transaction> GetMany(string Sku)
        {
            return this.GetAll().Where(x => x.Sku.Equals(Sku));
        }
    }
}
