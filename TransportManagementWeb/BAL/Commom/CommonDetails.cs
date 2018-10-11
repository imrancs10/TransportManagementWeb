using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using TransportManagementWeb.Models.Masters;

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

        public List<ServiceOrderDetail> GetAllReferenceIdsForTrashipmentAlottment()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.ServiceOrderDetails.Where(x => x.VendorId == null).ToList();
        }
        public List<ServiceOrderDetail> GetAllReferenceIdsForFreightPage()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.ServiceOrderDetails.Where(x => x.VendorId != null && !x.ServiceOrderPaymentDetails.Any()).ToList();
        }
        public List<ServiceOrderDetail> GetAllReferenceIdsForLRPage()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.ServiceOrderDetails.Include(x => x.ServiceOrderPaymentDetails).Where(x => x.VendorId != null && x.ServiceOrderPaymentDetails.Any() && !x.LRDetails.Any()).ToList();
        }
        public ServiceOrderModel GetServiceOrderDetail(int Id)
        {
            _db = new TransportManagementEntities();
            //_db.Configuration.LazyLoadingEnabled = false;
            var data = _db.ServiceOrderDetails
                        .Where(x => x.Id == Id)
                        .FirstOrDefault();
            ServiceOrderModel model = new ServiceOrderModel()
            {
                ClientName = data.ClientDetail.ClientName,
                GrossWeight = data.GrossWeight,
                NatureOfGoods = data.NatureOfGoods,
                VehicleName = data.VehicleDetail.VehicleName,
                VehicleRequirementDate = data.VehicleRequirementDate.Value.ToShortDateString(),
                VehicleTypeName = data.VehicleDetail.VehicleType.VehicleTypeName,
                Transhipments = new List<TranshipmentModel>()
            };
            foreach (var transhipment in data.TranshipmentDetails)
            {
                model.Transhipments.Add(new TranshipmentModel()
                {
                    FromCity = transhipment.City.CityName,
                    ToCity = transhipment.City1.CityName,
                    UnitName = transhipment.UnitDetail != null ? transhipment.UnitDetail.Name : string.Empty,
                    Weight = transhipment.WeightLookup != null ? transhipment.WeightLookup.Weight : string.Empty
                });
            }

            return model;
        }
        public List<VendorDetail> GetAllVendorDetail()
        {
            _db = new TransportManagementEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            var result = _db.VendorDetails
                             .Select(x => new
                             {
                                 VendorName = x.VendorName,
                                 VendorId = x.VendorId,
                             }).ToList()
                            .Select(v => new VendorDetail()
                            {
                                VendorName = v.VendorName,
                                VendorId = v.VendorId,
                            }).ToList();

            return result;

        }
    }
}