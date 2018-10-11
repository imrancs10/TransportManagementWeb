using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportManagementWeb.Models.Masters
{
    public class ServiceOrderModel
    {
        public string ClientName { get; set; }
        public string VehicleRequirementDate { get; set; }
        public List<TranshipmentModel> Transhipments { get; set; }
        public string VehicleTypeName { get; set; }
        public string VehicleName { get; set; }
        public string GrossWeight { get; set; }
        public string NatureOfGoods { get; set; }
    }

    public class TranshipmentModel
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string Weight { get; set; }
        public string UnitName { get; set; }
    }
}