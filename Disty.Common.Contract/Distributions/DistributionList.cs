using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Common.Contract.Distributions
{
    [Serializable]
    public class DistributionList
    {
        public DistributionList()
        {
            Id = 0;
        }


        public int Id { get; set; }

        public string Creator { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        
    }
}
