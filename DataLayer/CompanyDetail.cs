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
    
    public partial class CompanyDetail
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> PinCode { get; set; }
        public Nullable<int> MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
    
        public virtual City City { get; set; }
        public virtual State State { get; set; }
    }
}
