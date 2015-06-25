using System;
using System.Runtime.Serialization;

namespace Disty.Common.Contract
{
    [Serializable, DataContract]
    public abstract class DistyEntity
    {
        [DataMember]
        public int Id { get; set; }
    }
}