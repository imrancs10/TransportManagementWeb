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
    
    public partial class ClientBillDescription
    {
        public int Id { get; set; }
        public Nullable<int> ClientLeadgerDetailId { get; set; }
        public string Description { get; set; }
        public string SACCode { get; set; }
        public Nullable<int> ConsighmentNumber { get; set; }
        public string Quantity { get; set; }
        public string Charges { get; set; }
        public string Total { get; set; }
        public string DiscountPercentage { get; set; }
        public string Discount { get; set; }
        public string TotalAmount { get; set; }
    
        public virtual LRDetail LRDetail { get; set; }
        public virtual ClientBillDetail ClientBillDetail { get; set; }
    }
}
