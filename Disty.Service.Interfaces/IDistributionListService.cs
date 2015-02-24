using Disty.Common.Contract.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Service.Interfaces
{
    public interface IDistributionListService
    {

        DistributionList Get(Guid id);

    }
}
