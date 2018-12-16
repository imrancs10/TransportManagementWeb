using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportManagementWeb.Models.Masters
{
    public class ClientBillDetailModel
    {
        public ClientBillDetailModel()
        {
            this.ClientBillDescriptions = new List<ClientBillDescriptionModel>();
        }

        public int Id { get; set; }
        public Nullable<int> ClientId { get; set; }
        public string DocketCharge { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string LoadingCharge { get; set; }
        public string Cess { get; set; }
        public string Tax { get; set; }
        public string RoundOff { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string UnloadingCharge { get; set; }
        public string InvoiceNumber { get; set; }
        public string AdvanceAmount { get; set; }
        public List<ClientBillDescriptionModel> ClientBillDescriptions { get; set; }

    }
}