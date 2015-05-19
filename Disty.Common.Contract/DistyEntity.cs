using System;

namespace Disty.Common.Contract
{
    [Serializable]
    public abstract class DistyEntity
    {
        public int Id { get; set; }
    }
}