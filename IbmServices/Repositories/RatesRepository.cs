using IbmServices.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Linq;
using System.Threading.Tasks;

namespace IbmServices.Repositories
{
    public class RatesRepository : BaseRepository<Rate>
    {
        #region Constructors
        public RatesRepository(LogFactory Factory) : base(Factory.GetCurrentClassLogger())
        {
        }

        public RatesRepository(DbContextOptions<IbmContext> Options, LogFactory Factory) : base(Options, Factory.GetCurrentClassLogger())
        {
        }
        #endregion

        public override IQueryable<Rate> GetAll()
        {
            return this.Rates.AsQueryable();
        }

        public override Rate GetSingle(long ID)
        {
            return this.Rates.Find(ID);
        }

        public override async Task<Rate> GetSingleAsync(long ID)
        {
            return await this.Rates.FindAsync(ID);
        }

        public override bool Update(Rate Entity)
        {
            this.Rates.Update(Entity);

            return true;
        }

        public override bool Delete(Rate Entity)
        {
            this.Rates.Remove(Entity);

            return true;
        }
    }
}
