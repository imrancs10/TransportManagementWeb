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
        public Enums.CrudStatus AllotVendor(int vendorId, int referenceId)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            string referenceNo = VerificationCodeGeneration.GetSerialNumber();
            var _deptRow = _db.ServiceOrderDetails.Where(x => x.Id.Equals(referenceId)).FirstOrDefault();
            if (_deptRow != null)
            {
                _deptRow.VendorId = vendorId;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataNotFound;
        }
        public Enums.CrudStatus SaveServiceOrderPaymentDetail(int ReferenceId, string TotalFreight, string AdvanceFreight, string BalanceFreight, string LoadingCharge, string UnloadingCharge, string OthersCharge)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            var _deptRow = _db.ServiceOrderPaymentDetails.Where(x => x.ServiceOrderId == ReferenceId).FirstOrDefault();
            if (_deptRow == null)
            {
                ServiceOrderPaymentDetail _neworder = new ServiceOrderPaymentDetail();
                _neworder.AdvanceFreight = Convert.ToDecimal(AdvanceFreight);
                _neworder.BalanceFreight = Convert.ToDecimal(BalanceFreight);
                _neworder.LoadingCharge = Convert.ToDecimal(LoadingCharge);
                _neworder.OthersCharge = Convert.ToDecimal(OthersCharge);
                _neworder.TotalFreight = Convert.ToDecimal(TotalFreight);
                _neworder.UnloadingCharge = Convert.ToDecimal(UnloadingCharge);
                _neworder.ServiceOrderId = ReferenceId;
                _db.Entry(_neworder).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }

        public Enums.CrudStatus SaveLRDetail(int ReferenceId, string LRDate, string ConsignorName, string ConsignorAddress, string ConsignorCity, string ConsignorPincode, string ConsignorContact, string ConsignorGST, string ConsigneeName, string ConsigneeAddress, string ConsigneeCity, string ConsigneePincode, string ConsigneeContact, string ConsigneeGST, string productDetail, string GoodsDetail, string NoofUnit, string ChargeWeight, string InvoiceNo, string InvoiceValue, string EWayBillNo, string Remarks, string OwnerName, string OwnerContactNo, byte[] OwnerDLScanImage, byte[] OwnerpanNumberScanImage, byte[] OwnerRCScanImage, byte[] OwnerInsuranceScanImage, string DriverName, string DriverContactNo, byte[] DriverDLScanImage, byte[] DriverpanNumberScanImage, byte[] OtherScanAttachment)
        {
            string lrNumber = VerificationCodeGeneration.GetSerialNumber();
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            var _deptRow = _db.LRDetails.Where(x => x.ServiceOrderId == ReferenceId).FirstOrDefault();
            if (_deptRow == null)
            {
                LRDetail _neworder = new LRDetail();
                _neworder.ConsigneeDetails.Add(new ConsigneeDetail()
                {
                    CityId = Convert.ToInt32(ConsigneeCity),
                    ConsigneeAddress = ConsigneeAddress,
                    ConsigneeName = ConsigneeName,
                    ContactNo = ConsigneeContact,
                    GSTNo = ConsigneeGST,
                    Pincode = Convert.ToInt32(ConsigneePincode)
                });
                _neworder.ConsignorDetails.Add(new ConsignorDetail()
                {
                    CityId = Convert.ToInt32(ConsignorCity),
                    ConsignorAddress = ConsignorAddress,
                    ConsignorName = ConsignorName,
                    ContactNo = ConsignorContact,
                    GSTNo = ConsignorGST,
                    Pincode = Convert.ToInt32(ConsignorPincode)
                });
                _neworder.LRDate = Convert.ToDateTime(LRDate);
                _neworder.LRNumber = lrNumber;
                _neworder.OthersAttachment = OtherScanAttachment;
                _neworder.ProductDetails.Add(new ProductDetail()
                {
                    ChargeWeight = ChargeWeight,
                    EWayBillNo = EWayBillNo,
                    GoodsDetail = GoodsDetail,
                    InvoiceNo = InvoiceNo,
                    InvoiceValue = InvoiceValue,
                    NoofUnit = Convert.ToInt32(NoofUnit),
                    ProductDetails = productDetail,
                    Remarks = Remarks
                });
                _neworder.ServiceOrderId = ReferenceId;
                _neworder.VehicleDriverDetails.Add(new VehicleDriverDetail()
                {
                    ContactNo = DriverContactNo,
                    DLScanCopy = DriverDLScanImage,
                    DriverName = DriverName,
                    PANScanCopy = DriverpanNumberScanImage
                });
                _neworder.VehicleOwnerDetails.Add(new VehicleOwnerDetail()
                {
                    ContactNo = OwnerContactNo,
                    DLScanCopy = OwnerDLScanImage,
                    InsuranceScanCopy = OwnerInsuranceScanImage,
                    OwnerName = OwnerName,
                    PANScanCopy = OwnerpanNumberScanImage,
                    RCScanCopy = OwnerRCScanImage
                });
                _db.Entry(_neworder).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
    }
}