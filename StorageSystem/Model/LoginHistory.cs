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
    
    public partial class LoginHistory
    {
        public int LoginHistoryId { get; set; }
        public int UserId { get; set; }
        public System.DateTime LoginDatetime { get; set; }
        public Nullable<System.DateTime> ExitDatetime { get; set; }
    
        public virtual User User { get; set; }
    }
}
