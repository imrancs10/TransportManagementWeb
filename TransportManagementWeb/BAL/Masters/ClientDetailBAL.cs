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
        public Enums.CrudStatus BillEntrySave(ClientBillDetailModel model)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            ClientBillDetail _newclient = new ClientBillDetail();
            _newclient.AdvanceAmount = model.AdvanceAmount;
            _newclient.Cess = model.Cess;
            _newclient.CGST = model.CGST;
            _newclient.ClientId = model.ClientId;
            _newclient.DocketCharge = model.DocketCharge;
            _newclient.IGST = model.IGST;
            _newclient.InvoiceDate = model.InvoiceDate;
            _newclient.InvoiceNumber = getBillInvoiceNumber();
            _newclient.LoadingCharge = model.LoadingCharge;
            _newclient.RoundOff = model.RoundOff;
            _newclient.SGST = model.SGST;
            _newclient.Tax = model.Tax;
            _newclient.UnloadingCharge = model.UnloadingCharge;
            _newclient.ClientBillDescriptions.Add(new ClientBillDescription()
            {
                Charges = model.ClientBillDescriptions[0].Charges,
                ConsighmentNumber = Convert.ToInt32(model.ClientBillDescriptions[0].ConsighmentNumber),
                Discount = model.ClientBillDescriptions[0].Discount,
                Description = model.ClientBillDescriptions[0].Description,
                DiscountPercentage = model.ClientBillDescriptions[0].DiscountPercentage,
                Quantity = model.ClientBillDescriptions[0].Quantity,
                SACCode = model.ClientBillDescriptions[0].SACCode,
                Total = model.ClientBillDescriptions[0].Total,
                TotalAmount = model.ClientBillDescriptions[0].TotalAmount,
            });
            _db.Entry(_newclient).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        private string getBillInvoiceNumber()
        {
            string invoiceNo = string.Empty;
            _db = new TransportManagementEntities();
            int maxInvoiceId = _db.ClientBillDetails.OrderByDescending(x => x.Id).FirstOrDefault().Id;
            string finacialYear = DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") + "-" + DateTime.Now.AddYears(1).ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy") + "-" + DateTime.Now.ToString("yy");
            invoiceNo += "DIL/" + finacialYear + "/" + (maxInvoiceId + 1).ToString();
            return invoiceNo;
        }

    }
}