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
    public class LedgerEntryDetailBAL
    {
        TransportManagementEntities _db = null;
        public List<LedgerEntry> GetLedgerReportData(int? clientId, DateTime? fromDate, DateTime? toDate)
        {
            _db = new TransportManagementEntities();
            if (clientId == null && fromDate == null && toDate == null)
            {
                var data = _db.LedgerEntries
                                    .Include("LRDetail")
                                    .Include("ClientBillDetail")
                                    .OrderBy(x => x.CreatedDate)
                                    .ToList();
                return data;
            }
            else
            {
                if (clientId != null && fromDate != null && toDate != null)
                {
                    var data = _db.LedgerEntries
                                      .Include("LRDetail")
                                      .Include("ClientBillDetail")
                                      .Where(x => DbFunctions.TruncateTime(x.LedgerDate) >= DbFunctions.TruncateTime(fromDate)
                                      && DbFunctions.TruncateTime(x.LedgerDate) <= DbFunctions.TruncateTime(toDate)
                                      && x.ClientBillDetail.ClientId == clientId)
                                      .OrderBy(x => x.CreatedDate)
                                      .ToList();
                    return data;
                }
                else if (clientId != null)
                {
                    var data = _db.LedgerEntries
                                   .Include("LRDetail")
                                   .Include("ClientBillDetail")
                                   .Where(x => x.ClientBillDetail.ClientId == clientId)
                                   .OrderBy(x => x.CreatedDate)
                                   .ToList();
                    return data;
                }
                else if (fromDate != null && toDate != null)
                {
                    var data = _db.LedgerEntries
                                   .Include("LRDetail")
                                   .Include("ClientBillDetail")
                                   .Where(x => DbFunctions.TruncateTime(x.LedgerDate) >= DbFunctions.TruncateTime(fromDate)
                                   && DbFunctions.TruncateTime(x.LedgerDate) <= DbFunctions.TruncateTime(toDate))
                                   .OrderBy(x => x.CreatedDate)
                                   .ToList();
                    return data;
                }
            }
            return null;
        }
        public Enums.CrudStatus SaveLedgerEntryDetail(string LRId, string InvoiceId, string LedgerDate, string Description, string TransactionType, string TransactionAmount, string BalenceAmount)
        {
            _db = new TransportManagementEntities();
            int _effectRow = 0;
            LedgerEntry _newLedgerEntry = new LedgerEntry();
            _newLedgerEntry.LRId = int.Parse(LRId);
            _newLedgerEntry.InvoiceId = int.Parse(InvoiceId);
            _newLedgerEntry.LedgerDate = Convert.ToDateTime(LedgerDate);
            _newLedgerEntry.CreatedDate = DateTime.Now;
            _newLedgerEntry.Description = Description;
            _newLedgerEntry.TransactionType = TransactionType;
            _newLedgerEntry.TransactionAmount = int.Parse(TransactionAmount);
            _newLedgerEntry.BalenceAmount = Convert.ToDecimal(BalenceAmount);
            _db.Entry(_newLedgerEntry).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

    }
}