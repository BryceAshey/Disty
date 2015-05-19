using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Disty.Common.Contract;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data;
using log4net;

namespace Disty.Model.MySql.Repositories
{
    public abstract class RepositoryBase<TEntity, TSetEntity> : IRepository<TEntity, TSetEntity> 
        where TEntity : DistyEntity
        where TSetEntity : class
    {
        internal readonly ILog _log;

        protected RepositoryBase(ILog log)
        {
            _log = log;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            using (var db = new DistyModelContainer())
            {
                return
                    await
                        Task.FromResult<IEnumerable<TEntity>>(
                            db.Set<TSetEntity>()
                                .AsEnumerable()
                                .Select(Mapper.Map<TSetEntity, TEntity>)
                                .ToList());
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            using (var db = new DistyModelContainer())
            {
                var query = from r in db.Lists
                    where r.Id == id
                    orderby r.Name
                    select r;

                return await query.Project().ToFirstOrDefaultAsync<TEntity>();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TSetEntity, bool>> expression)
        {
            using (var db = new DistyModelContainer())
            {
                return await QueryAsync(expression, db);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TSetEntity, bool>> expression, DistyModelContainer db)
        {
            if(db == null)
                throw new ArgumentNullException("db");

            return await db.Set<TSetEntity>().Where(expression).Project().ToListAsync<TEntity>();
        }

        public virtual async Task<int> SaveAsync(TEntity item)
        {
            using (var db = new DistyModelContainer())
            {
                var dbItem = Mapper.Map<TEntity, TSetEntity>(item);
                if (item.Id == 0)
                {
                    db.Set<TSetEntity>().Add(dbItem);
                }
                else
                {
                    db.Set<TSetEntity>().Attach(dbItem);
                    db.Entry(item).State = EntityState.Modified;
                }

                return await db.SaveChangesAsync();
            }
        }
    }
}