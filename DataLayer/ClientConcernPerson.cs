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
    
    public partial class ClientConcernPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<long> ContactNumber { get; set; }
        public string EmailId { get; set; }
        public Nullable<int> ClientId { get; set; }
    
        public virtual ClientDetail ClientDetail { get; set; }
    }
}
