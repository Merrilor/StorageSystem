//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StorageSystem.Settings.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThemeSettings
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ThemeId { get; set; }
    
        public virtual Theme Theme { get; set; }
    }
}
