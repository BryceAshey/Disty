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
        private readonly IDistyClient _client;

        public ListClient(IDistyClient client)
        {
            _client = client;
        }

        public async void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DistributionList>> GetAsync()
        {
            var result = await _client.GetAsync("api/distributionList");

            if(result.IsSuccessStatusCode)
            {
                var body = await result.Content.ReadAsStringAsync();

            }

            throw new NotImplementedException();
        }

        public async Task<DistributionList> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync(DistributionList item)
        {
            throw new NotImplementedException();
        }
    }
}
