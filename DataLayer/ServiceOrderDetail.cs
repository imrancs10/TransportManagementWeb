//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceOrderDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceOrderDetail()
        {
            this.TranshipmentDetails = new HashSet<TranshipmentDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<System.DateTime> VehicleRequirementDate { get; set; }
        public Nullable<int> VehicleDetailId { get; set; }
        public string GrossWeight { get; set; }
        public string NatureOfGoods { get; set; }
        public string Freight { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ReferenceNumber { get; set; }
    
        public virtual ClientDetail ClientDetail { get; set; }
        public virtual VehicleDetail VehicleDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TranshipmentDetail> TranshipmentDetails { get; set; }
    }
}
