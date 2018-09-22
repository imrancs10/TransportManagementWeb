using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using TransportManagementWeb.Global;
using TransportManagementWeb.Models.Masters;

namespace TransportManagementWeb.BAL.Masters
{
    public class DoctorDetails
    {
        TransportManagementWebEntities _db = null;
        public Enums.CrudStatus SaveDoctor(string doctorName,int deptId)
        {
            _db = new TransportManagementWebEntities();
            int _effectRow = 0;
            var _deptRow = _db.Doctors.Where(x => x.DoctorName.Equals(doctorName) && x.DepartmentID.Equals(deptId)).FirstOrDefault();
            if (_deptRow == null)
            {
                Doctor _newDoc = new Doctor();
                _newDoc.DoctorName = doctorName;
                _newDoc.DepartmentID = deptId;
                _db.Entry(_newDoc).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
        public Enums.CrudStatus EditDoctor(string doctorName, int deptId,int docId)
        {
            _db = new TransportManagementWebEntities();
            int _effectRow = 0;
            var _docRow = _db.Doctors.Where(x => x.DoctorID.Equals(docId)).FirstOrDefault();
            if (_docRow != null)
            {
                _docRow.DoctorName = doctorName;
                _docRow.DepartmentID = deptId;
                _db.Entry(_docRow).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
            }
            else
                return Enums.CrudStatus.DataNotFound;
        }
        public Enums.CrudStatus DeleteDoctor(int docId)
        {
            _db = new TransportManagementWebEntities();
            int _effectRow = 0;
            var _docRow = _db.Doctors.Where(x => x.DoctorID.Equals(docId)).FirstOrDefault();
            if (_docRow != null)
            {
                _db.Doctors.Remove(_docRow);
                //_db.Entry(_deptRow).State = EntityState.Deleted;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Deleted : Enums.CrudStatus.NotDeleted;
            }
            else
                return Enums.CrudStatus.DataNotFound;
        }
        public List<DoctorModel> DoctorList()
        {
            _db = new TransportManagementWebEntities();
            var _list = (from doc in _db.Doctors
                         from dept in _db.Departments.Where(x=>x.DepartmentID.Equals(doc.DepartmentID))
                         orderby dept.DepartmentName
                         select new DoctorModel
                         {
                             DoctorName = doc.DoctorName,
                             DepartmentId = dept.DepartmentID,
                             DoctorId=doc.DoctorID,
                             DepartmentName=dept.DepartmentName
                         }).ToList();
            return _list != null ? _list : new List<DoctorModel>();
        }
    }
}