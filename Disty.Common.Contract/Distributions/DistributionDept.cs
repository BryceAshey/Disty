using System;
using System.Runtime.Serialization;
namespace Disty.Common.Contract.Distributions
{
    [Serializable, DataContract]
    public class DistributionDept : DistyEntity
    {
        [DataMember]
        public string Name { get; set; }
    }
}