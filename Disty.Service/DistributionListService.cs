using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Service.Interfaces;
using log4net;

namespace Disty.Service
{
    public class DistributionListService : IDistributionListService
    {
        private ILog _log;

        public DistributionListService(ILog log)
        {
            _log = log;
        }

        public DistributionList Get(Guid id)
        {
            return new DistributionList()
            {
                Id = id,
                Creator = "bashey",
                Name = "Test List",
                Owner = "bashey"
            };
        }

    }
}
