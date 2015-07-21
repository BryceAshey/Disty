using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Disty.Common.Contract;

namespace Disty.Common.Data
{
    public interface IRepository<TEntity> 
        where TEntity : DistyEntity
    {
        void DeleteAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task<int> SaveAsync(TEntity list);
    }
}