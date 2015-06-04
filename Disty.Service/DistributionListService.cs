using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Model.MySql.Repositories;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service
{
    public class DistributionListService : IDistributionListService
    {
        private readonly ILog _log;
        private readonly IDistributionListRepository _repository;

        public DistributionListService(ILog log, IDistributionListRepository repository)
        {
            _log = log;
            _repository = repository;
        }

        public async Task<IEnumerable<DistributionList>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<DistributionList> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<int> SaveAsync(DistributionList list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            list.Creator = "bashey";
            list.Dept = "eCAC";
            list.Owner = "bashey";

            return await _repository.SaveAsync(list);
        }
    }
}