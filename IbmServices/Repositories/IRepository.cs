using IbmServices.Models;
using System.Linq;
using System.Threading.Tasks;

namespace IbmServices.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T GetSingle(long ID);
        Task<T> GetSingleAsync(long ID);
        long Create(T Entity);
        bool Update(T Entity);
        bool Delete(T Entity);
        void Commit();
        Task CommitAsync();
    }
}
