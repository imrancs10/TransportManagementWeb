using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

namespace TransportManagementWeb.BAL.Commom
{
    public class CommonDetails
    {
        TransportManagementEntities _db = null;

        public List<Country> CountryList()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            var _list = _db.Countries.ToList();
            return _list != null ? _list : new List<Country>();
        }

        public List<State> GetStateByCountryId(int countryId)
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.States.Where(x => x.CountryId == countryId).ToList();
        }
        public List<City> GetCityByStateId(int stateId)
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.Cities.Where(x => x.StateId == stateId).ToList();
        }
    }
}