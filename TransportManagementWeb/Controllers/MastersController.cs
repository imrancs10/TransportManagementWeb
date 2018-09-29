using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult SaveVendorDetail(string vendorName, string address1, string address2, string area, string pincode, string city, string state, string country, string gstNo, string panNumber, string CP1EMail, string CP1ContactNo, string CP2Email, string CP2ContactNo, string BankName, string AccountNo, string AccountHolderName, string IFSCCode, string BankAddress)
        {
            VendorDetailBAL _details = new VendorDetailBAL();
            _details.SaveVendorDetail(vendorName, address1, address2, area, pincode, city, state, country, gstNo, panNumber, CP1EMail, CP1ContactNo, CP2Email, CP2ContactNo, BankName, AccountNo, AccountHolderName, IFSCCode, BankAddress);
            SetAlertMessage("Vendor detail added succesfully.", "Vendor Master");
            return RedirectToAction("VendorMaster");
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
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Home", "Login");
        }
    }
}