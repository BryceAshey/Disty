using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data;

namespace Disty.Common.Data.Repositories
{
    public interface IDistributionDeptRepository : IRepository<DistributionDept>
    {
        //Task<IEnumerable<DistributionDept>> GetByNameAsync(string name);
    }
}