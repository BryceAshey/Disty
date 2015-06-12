using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Disty.Common.Contract;

namespace Disty.Common.Data
{
    public interface IRepository<TEntity, TSetEntity> 
        where TEntity : DistyEntity
        where TSetEntity : class
    {
        void DeleteAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task<int> SaveAsync(TEntity list);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TSetEntity, bool>> expression);
    }
}