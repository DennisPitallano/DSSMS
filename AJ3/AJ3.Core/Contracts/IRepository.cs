using System.Threading.Tasks;

namespace AJ3.Core.Contracts
{
    public interface IRepository<T, in TE>
    {
        Task<T> GetByIdAsync(object id);
        Task<T> CreateAsync(TE entity);
        Task<T> UpdateAsync(TE entity);
    }

    public interface IDelete
    {
        Task<bool> DeleteAsync(object id);
    }

    public interface IExist
    {
        Task<bool> ExistAsync(object id);
    }
}
