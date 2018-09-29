using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportManagementWeb.BAL.Masters;
using TransportManagementWeb.Models.Masters;

namespace TransportManagementWeb.Controllers
{
    public class MastersController : CommonController
    {
        DepartmentDetails _details = null;
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
        public ActionResult SaveClientDetail(string clientName,string address1,string address2,string area,string pincode,string city,string state,string country,string gstNo,string panNumber,string CP1EMail,string CP1ContactNo,string CP2Email,string CP2ContactNo)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.SaveDept(clientName)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditDepartment(string deptName, int deptId)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.EditDept(deptName, deptId)), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteDepartment(int deptId)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.DeleteDept(deptId)), JsonRequestBehavior.AllowGet);
        }
        public override JsonResult GetDepartments()
        {
            _details = new DepartmentDetails();
            return Json(_details.DepartmentList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Home", "Login");
        }
    }
}