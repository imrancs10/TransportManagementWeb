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
    
    public partial class TranshipmentDetail
    {
        public int Id { get; set; }
        public Nullable<int> FromCityId { get; set; }
        public Nullable<int> ToCityId { get; set; }
        public string Weight { get; set; }
        public Nullable<int> UnitId { get; set; }
    
        public virtual UnitDetail UnitDetail { get; set; }
    }
}