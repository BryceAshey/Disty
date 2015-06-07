using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract;

namespace Disty.Common.Service
{
    public abstract interface IBaseService<T> where T : DistyEntity
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<int> SaveAsync(T item);
    }
}
