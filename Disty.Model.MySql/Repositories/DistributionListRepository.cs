using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Disty.Common.Contract.Distributions;
using Disty.Common.Log.Exceptions;
using log4net;

namespace Disty.Model.MySql.Repositories
{
    public class DistributionListRepository : RepositoryBase<DistributionList, List>, IDistributionListRepository
    {
        private readonly IDistributionDeptRepository _deptRepository;

        public DistributionListRepository(ILog log, IDistributionDeptRepository deptRepository) : base(log)
        {
            _deptRepository = deptRepository;
        }

        public virtual async Task<IEnumerable<DistributionList>> GetAsync()
        {
            return await GetAsync("Emails");
        }

        public virtual async Task<int> SaveAsync(DistributionList item)
        {
            try
            {
                var deptList = await _deptRepository.GetByNameAsync(item.Dept);

                // TODO Rework this to pull it up into the service.  
                // TODO Also need to validate that the user has rights to save to this dept.
                using (var db = new DistyModelContainer())
                {
                    Dept dept;
                    if (deptList == null || !deptList.Any())
                    {
                        var deptId = await _deptRepository.SaveAsync(new DistributionDept() { Name = item.Dept });
                        dept = db.Depts.Find(deptId);
                    }
                    else
                    {
                        dept = db.Depts.Find(deptList.First().Id);
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

                    return await db.SaveChangesAsync();
                }
            }
            catch (DbEntityValidationException e)
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
                
                _log.Error(string.Format("Error saving Disty list:\r\n  {0}", msg), e);
                throw new LoggedException(msg, e);
            }
        }
    }
}