//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StorageSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerTag
    {
        public int CustomerTagId { get; set; }
        public int CustomerId { get; set; }
        public int TagId { get; set; }
        public System.DateTime AddedDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
