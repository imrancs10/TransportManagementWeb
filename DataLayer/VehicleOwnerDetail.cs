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
    
    public partial class VehicleOwnerDetail
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string ContactNo { get; set; }
        public byte[] DLScanCopy { get; set; }
        public byte[] PANScanCopy { get; set; }
        public byte[] RCScanCopy { get; set; }
        public byte[] InsuranceScanCopy { get; set; }
        public Nullable<int> LRId { get; set; }
    
        public virtual LRDetail LRDetail { get; set; }
    }
}
