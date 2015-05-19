using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Disty.Common.Contract.Distributions;
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

        public virtual async Task<int> SaveAsync(DistributionList item)
        {
            var deptList = await _deptRepository.GetByNameAsync(item.Dept);

            // TODO Rework this to pull it up into the service.  
            // TODO Also need to validate that the user has rights to save to this dept.
            using (var db = new DistyModelContainer())
            {
                Dept dept;
                if (deptList == null || deptList.Any())
                {
                    var deptId = await _deptRepository.SaveAsync(new DistributionDept() {Name = item.Dept});
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
    }
}