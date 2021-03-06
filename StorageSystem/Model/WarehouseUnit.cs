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
    
    public partial class WarehouseUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WarehouseUnit()
        {
            this.SaleOrderContent = new HashSet<SaleOrderContent>();
            this.SupplyOrder = new HashSet<SupplyOrder>();
        }
    
        public int WarehouseUnitId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int MinimalAmount { get; set; }
        public bool IsAvailable { get; set; }
    
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleOrderContent> SaleOrderContent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyOrder> SupplyOrder { get; set; }
    }
}
