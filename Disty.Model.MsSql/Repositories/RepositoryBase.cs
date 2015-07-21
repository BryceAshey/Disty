using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Disty.Common.Contract;
using Disty.Common.Data;
using log4net;

namespace Disty.Model.MsSql.Repositories
{
    public abstract class RepositoryBase<TEntity, TSetEntity> : IRepository<TEntity>
        where TEntity : DistyEntity
        where TSetEntity : class
    {
        internal readonly ILog _log;

        protected RepositoryBase(ILog log)
        {
            _log = log;
        }

        public virtual async void DeleteAsync(int id)
        {
            using (var db = new DistyEntities())
            {
                var obj = await db.Set<TSetEntity>().FindAsync(id);
                if (obj == null)
                    return;
                
                db.Set<TSetEntity>().Remove(obj);
                await db.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            using (var db = new DistyEntities())
            {
                return
                    await
                        Task.FromResult<IEnumerable<TEntity>>(
                            Enumerable.ToList(db.Set<TSetEntity>()
                                .AsEnumerable()
                                .Select(Mapper.Map<TSetEntity, TEntity>)));
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            using (var db = new DistyEntities())
            {
                return await Task.FromResult(
                    Mapper.Map<TSetEntity, TEntity>(db.Set<TSetEntity>()
                        .Find(id))
                    );
            }
        }

        //public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TSetEntity, bool>> expression)
        //{
        //    using (var db = new DistyModelContainer())
        //    {
        //        return await QueryAsync(expression, db);
        //    }
        //}

        public abstract Task<int> SaveAsync(TEntity item);

        public virtual async Task<IEnumerable<TEntity>> GetAsync(string includes)
        {
            using (var db = new DistyEntities())
            {
                return
                    await
                        Task.FromResult<IEnumerable<TEntity>>(
                            Enumerable.ToList(db.Set<TSetEntity>()
                                .Include(includes)
                                .AsEnumerable()
                                .Select(Mapper.Map<TSetEntity, TEntity>)));
            }
        }

        //public virtual async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TSetEntity, bool>> expression,
        //    DistyModelContainer db)
        //{
        //    if (db == null)
        //        throw new ArgumentNullException("db");

        //    return await db.Set<TSetEntity>().Where(expression).Project().ToListAsync<TEntity>();
        //}

        #region Internals

        protected internal string HandleValidationError(DbEntityValidationException e)
        {
            var msg = "";

            foreach (var eve in e.EntityValidationErrors)
            {
                msg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:\r\n",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    msg += string.Format("- Property: \"{0}\", Error: \"{1}\"\r\n",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }

            return msg;
        }

        #endregion
    }
}