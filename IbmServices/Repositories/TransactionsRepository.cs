using IbmServices.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Linq;
using System.Threading.Tasks;

namespace IbmServices.Repositories
{
    public class TransactionsRepository : BaseRepository<Transaction>
    {
        #region Constructors
        public TransactionsRepository(LogFactory Factory) : base(Factory.GetCurrentClassLogger())
        {
        }

        public TransactionsRepository(DbContextOptions<IbmContext> Options, LogFactory Factory) : base(Options, Factory.GetCurrentClassLogger())
        {
        }
        #endregion

        public override IQueryable<Transaction> GetAll()
        {
            return this.Transactions.AsQueryable();
        }

        public override Transaction GetSingle(long ID)
        {
            return this.Transactions.Find(ID);
        }

        public override async Task<Transaction> GetSingleAsync(long ID)
        {
            return await this.Transactions.FindAsync(ID);
        }

        public override bool Update(Transaction Entity)
        {
            this.Transactions.Update(Entity);

            return true;
        }

        public override bool Delete(Transaction Entity)
        {
            this.Transactions.Remove(Entity);

            return true;
        }
    }
}
