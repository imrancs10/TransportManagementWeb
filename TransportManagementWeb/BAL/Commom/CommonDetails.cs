using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using TransportManagementWeb.Models.Common;

namespace TransportManagementWeb.BAL.Commom
{
    public class CommonDetails
    {
        TransportManagementWebEntities _db = null;

        public List<DayModel> DaysList()
        {
            _db = new TransportManagementWebEntities();
            var _list = (from day in _db.DayMasters
                         select new DayModel
                         {
                             DayId = day.DayId,
                             DayName = day.DayName
                         }).ToList();
            return _list != null ? _list : new List<DayModel>();
        }
    }
}