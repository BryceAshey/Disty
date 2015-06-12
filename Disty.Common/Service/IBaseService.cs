using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract;

namespace Disty.Common.Service
{
    public interface IBaseService<T> where T : DistyEntity
    {
        void DeleteAsync(int id);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<int> SaveAsync(T item);
    }
}