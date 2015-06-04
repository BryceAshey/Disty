using System;

namespace Disty.Common.Contract.Distributions
{
    [Serializable]
    public class DistributionList : DistyEntity
    {
        public string Creator { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Dept { get; set; }
        public string Emails { get; set; }
    }
}