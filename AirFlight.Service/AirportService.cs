using AirFlight.Model;
using AirFlight.Repository.Interfaces;
using AirFlight.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirFlight.Service
{
    public class AirportService : IAirportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AirportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAirport(Airport airport)
        {
            _unitOfWork.AirportsRepository.Add(airport);
            _unitOfWork.Save();
        }

        public Airport AddAirport(string airportGeoPoint, string airportName)
        {
            var latLon = airportGeoPoint.Split(new char[] { '|' });
            var airport = new Airport()
            {
                Latitude = double.Parse(latLon[0]),
                Longitude = double.Parse(latLon[1]),
                Name = airportName
            };

            AddAirport(airport);

            return airport;
        }

        public void DeleteAirport(Airport airport)
        {
            _unitOfWork.AirportsRepository.Delete(airport);
            _unitOfWork.Save();
        }

        public void EditAirport(Airport airport)
        {
            _unitOfWork.AirportsRepository.Edit(airport);
            _unitOfWork.Save();
        }

        public Airport GetAirport(int airportId)
        {
            return _unitOfWork.AirportsRepository.FindById(airportId);
        }

        public Airport GetAirportBy(Func<Airport, bool> predicate)
        {
            return _unitOfWork.AirportsRepository.FindBy(predicate).FirstOrDefault();
        }

        public IList<Airport> GetAllAirports()
        {
            return _unitOfWork.AirportsRepository.GetAll();
        }
    }
}
