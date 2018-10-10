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
    
    public partial class VendorDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VendorDetail()
        {
            this.VendorBankDetails = new HashSet<VendorBankDetail>();
            this.VendorConcernPersons = new HashSet<VendorConcernPerson>();
            this.VendorLineHaulDetails = new HashSet<VendorLineHaulDetail>();
            this.ServiceOrderDetails = new HashSet<ServiceOrderDetail>();
        }
    
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Area { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> PinCode { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string GSTNNumber { get; set; }
        public string PanNumber { get; set; }
        public byte[] GSTNNumberScanCopy { get; set; }
        public byte[] PanNumberScanCopy { get; set; }
    
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorBankDetail> VendorBankDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorConcernPerson> VendorConcernPersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorLineHaulDetail> VendorLineHaulDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceOrderDetail> ServiceOrderDetails { get; set; }
    }
}
