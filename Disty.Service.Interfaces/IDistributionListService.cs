using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;

namespace Disty.Service.Interfaces
{
    public interface IDistributionListService
    {
        Task<IEnumerable<DistributionList>> GetAsync();
        Task<DistributionList> GetAsync(int id);
        Task<int> SaveAsync(DistributionList list);
    }
}