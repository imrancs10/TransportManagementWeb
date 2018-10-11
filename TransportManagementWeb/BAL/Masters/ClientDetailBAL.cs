using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using TransportManagementWeb.Global;

namespace TransportManagementWeb.BAL.Masters
{
    public class ClientDetailBAL
    {
        TransportManagementEntities _db = null;

        public Enums.CrudStatus SaveClientDetail(string clientName, string address1, string address2, string area, string pincode, string city, string state, string country, string gstNo, string panNumber, string CP1EMail, string CP1ContactNo, string CP2Email, string CP2ContactNo)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            var _deptRow = _db.ClientDetails.Where(x => x.ClientName.Equals(clientName)).FirstOrDefault();
            if (_deptRow == null)
            {
                ClientDetail _newclient = new ClientDetail();
                _newclient.ClientName = clientName;
                _newclient.Address1 = address1;
                _newclient.Address2 = address2;
                _newclient.Area = area;
                _newclient.CityId = int.Parse(city);
                _newclient.CountryId = int.Parse(country);
                _newclient.GSTNNumber = gstNo;
                _newclient.PanNumber = panNumber;
                _newclient.PinCode = int.Parse(pincode);
                _newclient.StateId = int.Parse(state);
                _newclient.ClientConcernPersons.Add(new ClientConcernPerson() { ContactNumber = CP1ContactNo, EmailId = CP1EMail });
                _newclient.ClientConcernPersons.Add(new ClientConcernPerson() { ContactNumber = CP2ContactNo, EmailId = CP2Email });
                _db.Entry(_newclient).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
        public Enums.CrudStatus EditDept(string deptName, int deptId)
        {
            _db = new TransportManagementEntities();
            //int _effectRow = 0;
            //var _deptRow = _db.Departments.Where(x => x.DepartmentID.Equals(deptId)).FirstOrDefault();
            //if (_deptRow != null)
            //{
            //    _deptRow.DepartmentName = deptName;
            //    _db.Entry(_deptRow).State = EntityState.Modified;
            //    _effectRow = _db.SaveChanges();
            //    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
            //}
            //else
            return Enums.CrudStatus.DataNotFound;
        }
        public Enums.CrudStatus DeleteDept(int deptId)
        {
            _db = new TransportManagementEntities();
            //int _effectRow = 0;
            //var _deptRow = _db.Departments.Where(x => x.DepartmentID.Equals(deptId)).FirstOrDefault();
            //if (_deptRow != null)
            //{
            //    _db.Departments.Remove(_deptRow);
            //    //_db.Entry(_deptRow).State = EntityState.Deleted;
            //    _effectRow = _db.SaveChanges();
            //    return _effectRow > 0 ? Enums.CrudStatus.Deleted : Enums.CrudStatus.NotDeleted;
            //}
            //else
            return Enums.CrudStatus.DataNotFound;
        }

    }
}