using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TransportManagementWeb.BAL.Commom;
using TransportManagementWeb.BAL.Masters;
using TransportManagementWeb.Global;
using TransportManagementWeb.Models.Masters;

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
        public ActionResult SaveTranshipmentAllotment(int VendorId, int ReferenceId, int ClientÌd)
        {
            ServiceOrderBAL detail = new ServiceOrderBAL();
            var result = detail.AllotVendor(VendorId, ReferenceId);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Vendor allotment added succesfully.", "Service Order");
                return RedirectToAction("VehicleFreightDetail", new { referenceId = ReferenceId, ClientÌd = ClientÌd });
            }
            return RedirectToAction("TranshipmentAllotment");
        }

        public ActionResult VehicleFreightDetail(string referenceId, string ClientÌd)
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveFreightDetail(int ReferenceId, string TotalFreight, string AdvanceFreight, string BalanceFreight, string LoadingCharge, string UnloadingCharge, string OthersCharge)
        {
            ServiceOrderBAL detail = new ServiceOrderBAL();
            detail.SaveServiceOrderPaymentDetail(ReferenceId, TotalFreight, AdvanceFreight, BalanceFreight, LoadingCharge, UnloadingCharge, OthersCharge);
            SetAlertMessage("Freight detail added succesfully.", "Service Order");
            return RedirectToAction("VehicleFreightDetail");
        }
        public ActionResult LRGeneration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveLRDetail(int ReferenceId, string LRDate, string ConsignorName, string ConsignorAddress, string ConsignorCity, string ConsignorPincode, string ConsignorContact, string ConsignorGST, string ConsigneeName, string ConsigneeAddress, string ConsigneeCity, string ConsigneePincode, string ConsigneeContact, string ConsigneeGST, string ProductDetails, string GoodsDetail, string NoofUnit, string ChargeWeight, string InvoiceNo, string InvoiceValue, string EWayBillNo, string Remarks, string OwnerName, string OwnerContactNo, HttpPostedFileBase OwnerDLScanImage, HttpPostedFileBase OwnerpanNumberScanImage, HttpPostedFileBase OwnerRCScanImage, HttpPostedFileBase OwnerInsuranceScanImage, string DriverName, string DriverContactNo, HttpPostedFileBase DriverDLScanImage, HttpPostedFileBase DriverpanNumberScanImage, HttpPostedFileBase OtherScanAttachment)
        {
            byte[] ownerDLImage = null;
            byte[] ownerpanImage = null;
            byte[] ownerRCImage = null;
            byte[] ownerInsuranceImage = null;

            byte[] driverDLImage = null;
            byte[] driverpanImage = null;

            byte[] otherScanAttachment = null;
            otherScanAttachment = serilizeImagetoByte(OtherScanAttachment, otherScanAttachment);
            driverpanImage = serilizeImagetoByte(DriverpanNumberScanImage, driverpanImage);
            driverDLImage = serilizeImagetoByte(DriverDLScanImage, driverDLImage);
            ownerInsuranceImage = serilizeImagetoByte(OwnerInsuranceScanImage, ownerInsuranceImage);
            ownerRCImage = serilizeImagetoByte(OwnerRCScanImage, ownerRCImage);
            ownerpanImage = serilizeImagetoByte(OwnerpanNumberScanImage, ownerpanImage);
            ownerDLImage = serilizeImagetoByte(OwnerDLScanImage, ownerDLImage);

            ServiceOrderBAL detail = new ServiceOrderBAL();
            var lrDetail = detail.SaveLRDetail(ReferenceId, LRDate, ConsignorName, ConsignorAddress, ConsignorCity, ConsignorPincode, ConsignorContact, ConsignorGST, ConsigneeName, ConsigneeAddress, ConsigneeCity, ConsigneePincode, ConsigneeContact, ConsigneeGST, ProductDetails, GoodsDetail, NoofUnit, ChargeWeight, InvoiceNo, InvoiceValue, EWayBillNo, Remarks, OwnerName, OwnerContactNo, ownerDLImage, ownerpanImage, ownerRCImage, ownerInsuranceImage, DriverName, DriverContactNo, driverDLImage, driverpanImage, otherScanAttachment);
            SetAlertMessage("LR Generated succesfully with LR Number " + lrDetail.LRNumber + ".", "Service Order");
            return RedirectToAction("LRGeneration");
        }

        private static byte[] serilizeImagetoByte(HttpPostedFileBase image, byte[] imageByte)
        {
            if (image != null && image.ContentLength > 0)
            {
                imageByte = new byte[image.ContentLength];
                image.InputStream.Read(imageByte, 0, image.ContentLength);
                var img = new WebImage(imageByte).Resize(2000, 2000, true, true);
                imageByte = img.GetBytes();
            }

            return imageByte;
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
        public JsonResult GetAllReferenceIds(int clientId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllReferenceIdsForTrashipmentAlottment(clientId));
        }
        [HttpPost]
        public JsonResult GetAllReferenceIdsForFreightPage(int clientId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllReferenceIdsForFreightPage(clientId));
        }
        [HttpPost]
        public JsonResult GetAllReferenceIdsForLRPage(int clientId)
        {
            CommonDetails _details = new CommonDetails();
            var result = JsonConvert.SerializeObject(_details.GetAllReferenceIdsForLRPage(clientId), Formatting.Indented,
                          new JsonSerializerSettings
                          {
                              ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                          });
            return Json(result);
        }
        [HttpPost]
        public JsonResult GetServiceOrderDetail(int Id)
        {
            try
            {
                CommonDetails _details = new CommonDetails();
                var data = _details.GetServiceOrderDetail(Id);
                var jsonResult = Json(data, JsonRequestBehavior.AllowGet);
                return jsonResult;
            }
            catch (Exception ex)
            {
                throw;
            }

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

        public ActionResult BillReport(string invoiceNo)
        {
            if (!string.IsNullOrEmpty(invoiceNo))
            {
                CommonDetails _details = new CommonDetails();
                ViewData["InvoiceDetail"] = _details.GetBillDetailByInvoiceId(Convert.ToInt32(invoiceNo));
            }
            return View();
        }
        public ActionResult BillEntry()
        {
            return View();
        }
        public ActionResult LRReport()
        {
            return View();
        }
        public ActionResult LRReport2()
        {
            return View();
        }
        public ActionResult LedgerReport()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetAllLRDetails(int clientId)
        {
            CommonDetails _details = new CommonDetails();
            var result = JsonConvert.SerializeObject(_details.GetAllLRDetailsByClientId(clientId), Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetLRDetailByLRId(int LRId)
        {
            CommonDetails _details = new CommonDetails();
            var result = JsonConvert.SerializeObject(_details.GetLRDetailByLRId(LRId), Formatting.Indented,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            return Json(result);
        }

        [HttpPost]
        public ActionResult BillEntrySave(ClientBillDetailModel model)
        {
            ClientDetailBAL detail = new ClientDetailBAL();
            var result = detail.BillEntrySave(model);
            SetAlertMessage("Data has been saved.", "Bill Entry");
            return RedirectToAction("BillEntry");
        }
        [HttpPost]
        public JsonResult GetAllInvoiceDetail()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetAllInvoiceDetail());
        }

        //[HttpPost]
        //public JsonResult GetBillDetailByInvoiceId(int invoiceId)
        //{
        //    CommonDetails _details = new CommonDetails();
        //    return Json(_details.GetBillDetailByInvoiceId(invoiceId));
        //}
    }
}