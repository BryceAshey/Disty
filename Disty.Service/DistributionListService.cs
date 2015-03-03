using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Common.Data;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service
{
    public class DistributionListService : IDistributionListService
    {
        private ILog _log;
        private IDataClient<DistributionList> _dataClient;

        public DistributionListService(ILog log, IDataClient<DistributionList> dataClient)
        {
            _log = log;
            _dataClient = dataClient;
        }

        public async Task<List<DistributionList>> GetAsync()
        {
            return await _dataClient.GetAsync();
        }

        public async Task<DistributionList> GetAsync(string id)
        {
            return await _dataClient.GetByIdAsync(id);
        }

        public async Task<DistributionList> SaveAsync(DistributionList list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            list.Dept = "eCAC";
            return await _dataClient.SaveAsync(list);
        }

    }
}
