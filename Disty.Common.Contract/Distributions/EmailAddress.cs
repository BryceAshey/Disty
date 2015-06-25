
using System;
using System.Runtime.Serialization;
namespace Disty.Common.Contract.Distributions
{
    [Serializable, DataContract]
    public class EmailAddress : DistyEntity
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int ListId { get; set; }
    }
}
