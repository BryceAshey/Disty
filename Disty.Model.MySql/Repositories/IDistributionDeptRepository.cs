using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data;

namespace Disty.Model.MySql.Repositories
{
    public interface IDistributionDeptRepository : IRepository<DistributionDept, Dept>
    {
        Task<IEnumerable<DistributionDept>> GetByNameAsync(string name);
    }
}