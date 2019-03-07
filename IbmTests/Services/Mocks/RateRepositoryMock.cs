using IbmServices.Models;
using IbmServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IbmTests.Services.Mocks
{
    public class RateRepositoryMock : IRepository<Rate>
    {
        private readonly List<Rate> Rates = new List<Rate>();

        public void Commit()
        {
            // Empty for testing purposes
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public long Create(Rate Entity)
        {
            Entity.Id = this.Rates.Count > 0 ? this.Rates.Last().Id + 1 : 1;

            this.Rates.Add(Entity);

            return Entity.Id;
        }

        public bool Delete(Rate Entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Rate> GetAll()
        {
            return this.Rates.AsQueryable();
        }

        public Rate GetSingle(long ID)
        {
            throw new NotImplementedException();
        }

        public Task<Rate> GetSingleAsync(long ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rate Entity)
        {
            Rate OldRate = this.Rates.Single(x => x.Id == Entity.Id);
            OldRate.RateVal = Entity.RateVal;

            return true;
        }
    }
}
