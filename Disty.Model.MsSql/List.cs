//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Disty.Model.MsSql
{
    using System;
    using System.Collections.Generic;
    
    public partial class List
    {
        public List()
        {
            this.Emails = new HashSet<Email>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Creator { get; set; }
        public int Dept_Id { get; set; }
    
        public virtual Dept Dept { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
    }
}
