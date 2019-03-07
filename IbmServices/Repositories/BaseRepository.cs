using IbmServices.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Linq;
using System.Threading.Tasks;

namespace IbmServices.Repositories
{
    public abstract class BaseRepository<T> : IbmContext, IRepository<T> where T : BaseEntity
    {
        protected readonly Logger _Logger;

        protected BaseRepository(Logger Logger)
        {
            this._Logger = Logger;
        }

        protected BaseRepository(DbContextOptions<IbmContext> Options, Logger Logger) : base(Options)
        {
            this._Logger = Logger;
        }

        public abstract IQueryable<T> GetAll();
        public abstract T GetSingle(long ID);
        public abstract Task<T> GetSingleAsync(long ID);
        public abstract bool Update(T Entity);
        public abstract bool Delete(T Entity);

        public virtual long Create(T Entity)
        {
            var Id = this.Add(Entity).Entity.Id;

            this._Logger.Debug($"New {typeof(T).Name} ID: {Id}");
            return Id;
        }

        public virtual void Commit()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbUpdateConcurrencyException DbCEx)
            {
                this._Logger.Error(DbCEx);
            }
            catch (DbUpdateException DbUEx)
            {
                this._Logger.Error(DbUEx);
            }
        }

        public virtual async Task CommitAsync()
        {
            try
            {
                await this.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException DbCEx)
            {
                this._Logger.Error(DbCEx);
            }
            catch (DbUpdateException DbUEx)
            {
                this._Logger.Error(DbUEx);
            }
        }
    }
}
