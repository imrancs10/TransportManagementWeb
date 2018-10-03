using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using TransportManagementWeb.Global;
using TransportManagementWeb.Infrastructure.Utility;

namespace TransportManagementWeb.BAL.Masters
{
    public class ServiceOrderBAL
    {
        TransportManagementEntities _db = null;

        public Enums.CrudStatus SaveServiceOrderDetail(int ClientId, string vehicleReqDate, int? CityTransshipmentFrom, int? CityTransshipmentTo, int? CityMultiTransshipmentFrom, int? CityMultiTransshipmentTo1, int? WeightDropdown1, int? UnitDropdown1, int? CityMultiTransshipmentTo2, int? WeightDropdown2, int? UnitDropdown2, int? VehicleTypeId, int? VehicleDetailId, string GrossWeight, string NatureOfGoods, string Freight)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            string referenceNo = VerificationCodeGeneration.GetSerialNumber();
            var _deptRow = _db.ServiceOrderDetails.Where(x => x.ReferenceNumber.Equals(referenceNo)).FirstOrDefault();
            if (_deptRow == null)
            {
                ServiceOrderDetail _neworder = new ServiceOrderDetail();
                _neworder.ClientId = ClientId;
                _neworder.CreatedDate = DateTime.Now;
                _neworder.Freight = Freight;
                _neworder.GrossWeight = GrossWeight;
                _neworder.NatureOfGoods = GrossWeight;
                _neworder.NatureOfGoods = NatureOfGoods;
                _neworder.ReferenceNumber = referenceNo;
                _neworder.VehicleDetailId = VehicleDetailId;
                _neworder.VehicleRequirementDate = DateTime.Parse(vehicleReqDate);
                if (CityTransshipmentFrom != null && CityTransshipmentTo != null)
                    _neworder.TranshipmentDetails.Add(new TranshipmentDetail() { FromCityId = CityTransshipmentFrom, ToCityId = CityTransshipmentTo });
                else if (CityMultiTransshipmentFrom != null && CityMultiTransshipmentTo1 != null && CityMultiTransshipmentTo2 != null)
                {
                    _neworder.TranshipmentDetails.Add(new TranshipmentDetail() { FromCityId = CityMultiTransshipmentFrom, ToCityId = CityMultiTransshipmentTo1, UnitId = UnitDropdown1, WeightId = WeightDropdown1 });
                    _neworder.TranshipmentDetails.Add(new TranshipmentDetail() { FromCityId = CityMultiTransshipmentFrom, ToCityId = CityMultiTransshipmentTo2, UnitId = UnitDropdown2, WeightId = WeightDropdown2 });
                }
                _db.Entry(_neworder).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }

    }
}