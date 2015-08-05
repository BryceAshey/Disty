using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Disty.Common.Contract.Distributions
{
    [Serializable, DataContract]
    public class DistributionList : DistyEntity, IComparable
    {
        public DistributionList()
        {
            Emails = new List<EmailAddress>();
        }

        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public string Dept { get; set; }
        [DataMember]
        public int DeptId { get; set; }
        [DataMember]
        public IEnumerable<EmailAddress> Emails { get; set; }

        public int CompareTo(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException("obj");

            if (this.GetType() != obj.GetType())
                throw new InvalidOperationException("obj is not a matching type.");

            return this.Name.CompareTo(((DistributionList)obj).Name);
        }
    }
}