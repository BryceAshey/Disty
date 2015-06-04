using System;
using System.Collections.Generic;

namespace Disty.Common.Contract.Distributions
{
    [Serializable]
    public class DistributionList : DistyEntity
    {
        public DistributionList()
        {
            Emails = new List<EmailAddress>();
        }

        public string Creator { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Dept { get; set; }
        public IEnumerable<EmailAddress> Emails { get; set; }
    }
}