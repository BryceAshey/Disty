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

        public List<DistributionList> Get()
        {
            var lists = new List<DistributionList>(){
                new DistributionList()
                {
                    Id = 1,
                    Creator = "bashey",
                    Name = "Test List",
                    Owner = "bashey"
                }
            };

            //TODO Sort

            return lists;
        }

        public DistributionList Get(int id)
        {
            return new DistributionList()
            {
                Id = id,
                Creator = "bashey",
                Name = "Test List",
                Owner = "bashey"
            };
        }

        public DistributionList Save(DistributionList list)
        {
            if (list.Id == 0)
                list.Id = 1;

            return list;
        }

    }
}
