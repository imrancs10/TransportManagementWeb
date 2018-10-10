using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TransportManagementWeb.BAL.Commom;
using TransportManagementWeb.BAL.Masters;

namespace TransportManagementWeb.Controllers
{
    public class MastersController : CommonController
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult ClientMaster()
        {
            return View();
        }
        public ActionResult VendorMaster()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveClientDetail(string clientName, string address1, string address2, string area, string pincode, string city, string state, string country, string gstNo, string panNumber, string CP1EMail, string CP1ContactNo, string CP2Email, string CP2ContactNo)
        {
            ClientDetailBAL _details = new ClientDetailBAL();
            _details.SaveClientDetail(clientName, address1, address2, area, pincode, city, state, country, gstNo, panNumber, CP1EMail, CP1ContactNo, CP2Email, CP2ContactNo);
            SetAlertMessage("Client detail added succesfully.", "Client Master");
            return RedirectToAction("ClientMaster");
        }

        [HttpPost]
        public ActionResult SaveVendorDetail(string vendorName, string address1, string address2, string area, string pincode, string city, string state, string country, string gstNo, string panNumber, string CP1EMail, string CP1ContactNo, string CP2Email, string CP2ContactNo, string BankName, string AccountNo, string AccountHolderName, string IFSCCode, string BankAddress, HttpPostedFileBase gstNoScanImage, HttpPostedFileBase panNumberScanImage)
        {
            VendorDetailBAL _details = new VendorDetailBAL();
            byte[] gstImage = null;
            byte[] panImage = null;
            if (gstNoScanImage != null && gstNoScanImage.ContentLength > 0)
            {
                gstImage = new byte[gstNoScanImage.ContentLength];
                gstNoScanImage.InputStream.Read(gstImage, 0, gstNoScanImage.ContentLength);
                var img = new WebImage(gstImage).Resize(2000, 2000, true, true);
                gstImage = img.GetBytes();
            }
            if (panNumberScanImage != null && panNumberScanImage.ContentLength > 0)
            {
                panImage = new byte[panNumberScanImage.ContentLength];
                panNumberScanImage.InputStream.Read(panImage, 0, panNumberScanImage.ContentLength);
                var img = new WebImage(panImage).Resize(2000, 2000, true, true);
                panImage = img.GetBytes();
            }
            _details.SaveVendorDetail(vendorName, address1, address2, area, pincode, city, state, country, gstNo, panNumber, CP1EMail, CP1ContactNo, CP2Email, CP2ContactNo, BankName, AccountNo, AccountHolderName, IFSCCode, BankAddress, gstImage, panImage);
            SetAlertMessage("Vendor detail added succesfully.", "Vendor Master");
            return RedirectToAction("VendorMaster");
        }

        public ActionResult ServiceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveServiceOrder(int ClientId, string vehicleReqDate, int? CityTransshipmentFrom, int? CityTransshipmentTo, int? CityMultiTransshipmentFrom, int? CityMultiTransshipmentTo1, int? WeightDropdown1, int? UnitDropdown1, int? CityMultiTransshipmentTo2, int? WeightDropdown2, int? UnitDropdown2, int? VehicleTypeId, int? VehicleDetailId, string GrossWeight, string NatureOfGoods, string Freight)
        {
            ServiceOrderBAL _details = new ServiceOrderBAL();
            _details.SaveServiceOrderDetail(ClientId, vehicleReqDate, CityTransshipmentFrom, CityTransshipmentTo, CityMultiTransshipmentFrom, CityMultiTransshipmentTo1, WeightDropdown1, UnitDropdown1, CityMultiTransshipmentTo2, WeightDropdown2, UnitDropdown2, VehicleTypeId, VehicleDetailId, GrossWeight, NatureOfGoods, Freight);
            SetAlertMessage("Service order detail added succesfully.", "Service Order");
            return RedirectToAction("ServiceOrder");
        }

        public ActionResult TranshipmentAllotment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveTranshipmentAllotment(int VendorId, int ReferenceId)
        {
            ServiceOrderBAL detail = new ServiceOrderBAL();
            detail.AllotVendor(VendorId, ReferenceId);
            SetAlertMessage("Vendor allotment added succesfully.", "Service Order");
            return RedirectToAction("TranshipmentAllotment");
        }
        
        public ActionResult VehicleFreightDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveFreightDetail(int ReferenceId, string TotalFreight, string AdvanceFreight, string BalanceFreight, string LoadingCharge, string UnloadingCharge, string OthersCharge)
        {
            ServiceOrderBAL detail = new ServiceOrderBAL();
            detail.SaveServiceOrderPaymentDetail(ReferenceId, TotalFreight, AdvanceFreight, BalanceFreight, LoadingCharge, UnloadingCharge, OthersCharge);
            SetAlertMessage("Freight detail added succesfully.", "Service Order");
            return RedirectToAction("TranshipmentAllotment");
        }
        public ActionResult LRGeneration()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetCountry()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.CountryList());
        }
        [HttpPost]
        public JsonResult GetStateByCountryId(int countryId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetStateByCountryId(countryId));
        }
        [HttpPost]
        public JsonResult GetCityByStateId(int stateId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetCityByStateId(stateId));
        }
        [HttpPost]
        public JsonResult GetAllCities()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllCities());
        }
        [HttpPost]
        public JsonResult GetAllClientDetail()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllClientDetail());
        }
        [HttpPost]
        public JsonResult GetAllWeightDetail()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllWeightDetail());
        }
        [HttpPost]
        public JsonResult GetAllUnitDetail()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllUnitDetail());
        }
        [HttpPost]
        public JsonResult GetAllVehicleType()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllVehicleType());
        }
        [HttpPost]
        public JsonResult GetAllVehicleDetail(int typeId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllVehicleDetail(typeId));
        }
        [HttpPost]
        public JsonResult GetAllReferenceIds()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllReferenceIds());
        }
        [HttpPost]
        public JsonResult GetServiceOrderDetail(int Id)
        {
            CommonDetails _details = new CommonDetails();
            var data = _details.GetServiceOrderDetail(Id);
            var result = JsonConvert.SerializeObject(data, Formatting.Indented,
                         new JsonSerializerSettings
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });

            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpPost]
        public JsonResult GetAllVendorDetail()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllVendorDetail());
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Home", "Login");
        }
    }
}