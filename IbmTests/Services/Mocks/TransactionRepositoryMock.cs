using IbmServices.Models;
using IbmServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IbmTests.Services.Mocks
{
    public class TransactionRepositoryMock : IRepository<Transaction>
    {
        private readonly List<Transaction> Transactions = new List<Transaction>();

        public void Commit()
        {
            // Empty for testing purposes
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public long Create(Transaction Entity)
        {
            Entity.Id = this.Transactions.Count > 0 ? this.Transactions.Last().Id + 1 : 1;

            this.Transactions.Add(Entity);

            return Entity.Id;
        }

        public bool Delete(Transaction Entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transaction> GetAll()
        {
            return this.Transactions.AsQueryable();
        }

        public Transaction GetSingle(long ID)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> GetSingleAsync(long ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Transaction Entity)
        {
            throw new NotImplementedException();
        }
    }
}
