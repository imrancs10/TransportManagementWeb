using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using TransportManagementWeb.Global;

namespace TransportManagementWeb.BAL.Masters
{
    public class VendorDetailBAL
    {
        TransportManagementEntities _db = null;

        public Enums.CrudStatus SaveVendorDetail(string vendorName, string address1, string address2, string area, string pincode, string city, string state, string country, string gstNo, string panNumber, string CP1EMail, string CP1ContactNo, string CP2Email, string CP2ContactNo, string BankName, string AccountNo, string AccountHolderName, string IFSCCode, string BankAddress)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            var _deptRow = _db.VendorDetails.Where(x => x.VendorName.Equals(vendorName)).FirstOrDefault();
            if (_deptRow == null)
            {
                VendorDetail _newclient = new VendorDetail();
                _newclient.VendorName = vendorName;
                _newclient.Address1 = address1;
                _newclient.Address2 = address2;
                _newclient.Area = area;
                _newclient.CityId = int.Parse(city);
                _newclient.CountryId = int.Parse(country);
                _newclient.GSTNNumber = gstNo;
                _newclient.PanNumber = panNumber;
                _newclient.PinCode = int.Parse(pincode);
                _newclient.StateId = int.Parse(state);
                _newclient.VendorConcernPersons.Add(new VendorConcernPerson() { ContactNumber = int.Parse(CP1ContactNo), EmailId = CP1EMail });
                _newclient.VendorConcernPersons.Add(new VendorConcernPerson() { ContactNumber = int.Parse(CP2ContactNo), EmailId = CP2Email });
                _newclient.VendorBankDetails.Add(new VendorBankDetail()
                {
                    AccountHolderName = AccountHolderName,
                    AccountNo = AccountNo,
                    BankName = BankName,
                    BranchAddress = BankAddress,
                    IFSCCode = IFSCCode
                });
                _db.Entry(_newclient).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
    }
}