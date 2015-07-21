using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data.Repositories;
using Disty.Common.Log.Exceptions;
using log4net;

namespace Disty.Model.MsSql.Repositories
{
    public class DistributionListRepository : RepositoryBase<DistributionList, List>, IDistributionListRepository
    {
        private readonly IDistributionDeptRepository _deptRepository;

        public DistributionListRepository(ILog log, IDistributionDeptRepository deptRepository) : base(log)
        {
            _deptRepository = deptRepository;
        }

        public override async Task<IEnumerable<DistributionList>> GetAsync()
        {
            return await GetAsync("Emails");
        }

        public override async Task<int> SaveAsync(DistributionList item)
        {
            try
            {
                using (var db = new DistyEntities())
                {
                    var dept = db.Depts.Find(item.DeptId);
                    if (dept == null)
                    {
                        var deptId = await _deptRepository.SaveAsync(new DistributionDept() { Name = item.Name });
                        dept = db.Depts.Find(deptId);
                    }

                    var dbItem = Mapper.Map<DistributionList, List>(item);
                    dbItem.Dept = dept;
                    if (item.Id == 0)
                    {
                        db.Set<List>().Add(dbItem);
                    }
                    else
                    {
                        db.Set<List>().Attach(dbItem);
                        db.Entry(item).State = EntityState.Modified;
                    }

                    await db.SaveChangesAsync();
                    return dbItem.Id;
                }
            }
            catch (DbEntityValidationException e)
            {
                var msg = HandleValidationError(e);
                _log.Error(string.Format("Error saving Disty list:\r\n  {0}", msg), e);
                throw new LoggedException(msg, e);
            }
        }
    }
}