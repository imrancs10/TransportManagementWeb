using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportManagementWeb.Models.Masters
{
    public class ClientBillDescriptionModel
    {
        public int Id { get; set; }
        public int? ClientLeadgerDetailId { get; set; }
        public string Description { get; set; }
        public string SACCode { get; set; }
        public string ConsighmentNumber { get; set; }
        public string Quantity { get; set; }
        public string Charges { get; set; }
        public string Total { get; set; }
        public string DiscountPercentage { get; set; }
        public string Discount { get; set; }
        public string TotalAmount { get; set; }

    }
}