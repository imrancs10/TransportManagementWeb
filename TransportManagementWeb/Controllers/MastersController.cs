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
        public ActionResult AddSchedule()
        {
            return View();
        }

        public ActionResult AddDepartments()
        {
            return View();
        }

        public ActionResult AddDoctors()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDepartment(string deptName)
        {
            _details = new DepartmentDetails();

            return Json(CrudResponse(_details.SaveDept(deptName)), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult SaveDoctor(string doctorName, int deptId)
        {
            DoctorDetails _details = new DoctorDetails();

            return Json(CrudResponse(_details.SaveDoctor(doctorName, deptId)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditDoctor(string deptName, int deptId, int docId)
        {
            DoctorDetails _details = new DoctorDetails();
            return Json(CrudResponse(_details.EditDoctor(deptName, deptId, docId)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDoctor(int docId)
        {
            DoctorDetails _details = new DoctorDetails();
            return Json(CrudResponse(_details.DeleteDoctor(docId)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDoctors()
        {
            DoctorDetails _details = new DoctorDetails();
            return Json(_details.DoctorList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSchedule(ScheduleModel model)
        {
            ScheduleDetails _details = new ScheduleDetails();
            return Json(CrudResponse(_details.SaveSchedule(model)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditSchedule(ScheduleModel model)
        {
            ScheduleDetails _details = new ScheduleDetails();
            return Json(CrudResponse(_details.EditSchedule(model)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSchedule(ScheduleModel model)
        {
            ScheduleDetails _details = new ScheduleDetails();

            return Json(CrudResponse(_details.DeleteSchedule(model.ScheduleId)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSchedule()
        {
            ScheduleDetails _details = new ScheduleDetails();
            return Json(_details.ScheduleList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Dashboard", "Home");
        }
    }
}