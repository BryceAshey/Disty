using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Common.Data
{
    public interface IDataClient<T> where T : class, new()
    {
        Task<List<T>> GetAsync();

        Task<T> GetByIdAsync(string id);

        Task<T> SaveAsync(T obj);

        Task<bool> DeleteAsync(T obj);
    }
}
