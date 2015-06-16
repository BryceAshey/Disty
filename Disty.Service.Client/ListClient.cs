using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Service.Interfaces;

namespace Disty.Service.Client
{
    public class ListClient : IDistributionListService
    {
        private readonly IDistyClient<DistributionList> _client;

        public ListClient(IDistyClient<DistributionList> client)
        {
            _client = client;
        }

        public async void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DistributionList>> GetAsync()
        {
            return await _client.GetAsync("api/distributionList");
        }

        public async Task<DistributionList> GetAsync(int id)
        {
            return await _client.FindAsync(string.Format("api/distributionList/{0}", id));
        }

        public async Task<int> SaveAsync(DistributionList item)
        {
            throw new NotImplementedException();
        }
    }
}
