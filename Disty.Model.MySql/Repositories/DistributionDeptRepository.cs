using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
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
    }
}