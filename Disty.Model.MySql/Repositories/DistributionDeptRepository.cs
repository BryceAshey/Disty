using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using AutoMapper;
using Disty.Common.Contract.Distributions;
using Disty.Common.Log.Exceptions;
using log4net;

namespace Disty.Model.MySql.Repositories
{
    public class DistributionDeptRepository : RepositoryBase<DistributionDept, Dept>, IDistributionDeptRepository
    {
        public DistributionDeptRepository(ILog log) : base(log)
        {
        }

        public async Task<IEnumerable<DistributionDept>> GetByNameAsync(string name)
        {
            using (var db = new DistyModelContainer())
            {
                return await QueryAsync(u => u.Name == name, db);
            }
        }

        public override async Task<int> SaveAsync(DistributionDept item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                using (var db = new DistyModelContainer())
                {
                    var dbItem = Mapper.Map<DistributionDept, Dept>(item);
                    if (item.Id == 0)
                    {
                        db.Set<Dept>().Add(dbItem);
                    }
                    else
                    {
                        db.Set<Dept>().Attach(dbItem);
                        db.Entry(item).State = EntityState.Modified;
                    }

                    await db.SaveChangesAsync();
                    return dbItem.Id;
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = HandleValidationError(e);
                _log.Error(string.Format("Error saving Disty list:\r\n  {0}", msg, e));
                throw new LoggedException(msg, e);
            }
        }
    }
}