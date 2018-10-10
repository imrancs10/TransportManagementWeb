using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Microsoft.EntityFrameworkCore;

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
        public List<City> GetAllCities()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.Cities.ToList();
        }
        public List<ClientDetail> GetAllClientDetail()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.ClientDetails.ToList();
        }
        public List<WeightLookup> GetAllWeightDetail()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.WeightLookups.ToList();
        }
        public List<UnitDetail> GetAllUnitDetail()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.UnitDetails.ToList();
        }

        public List<VehicleType> GetAllVehicleType()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.VehicleTypes.ToList();
        }
        public List<VehicleDetail> GetAllVehicleDetail(int typeId)
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.VehicleDetails.Where(x => x.VehicleTypeId == typeId).ToList();
        }

        public List<ServiceOrderDetail> GetAllReferenceIds()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.ServiceOrderDetails.ToList();
        }
        public ServiceOrderDetail GetServiceOrderDetail(int Id)
        {
            _db = new TransportManagementEntities();
            //_db.Configuration.ProxyCreationEnabled = false;
            return _db.ServiceOrderDetails
                .Where(x => x.Id == Id)
                .FirstOrDefault();
        }
        public List<VendorDetail> GetAllVendorDetail()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.VendorDetails.ToList();
        }
    }
}