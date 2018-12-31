using AirFlight.Model;
using AirFlight.Repository.Interfaces;
using AirFlight.Service.Interfaces;
using Geocoding;
using System.Collections.Generic;
using System.Linq;

namespace AirFlight.Service
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGeocoder _geocoder;

        public FlightService(IUnitOfWork unitOfWork, IGeocoder geocoder)
        {
            _unitOfWork = unitOfWork;
            _geocoder = geocoder;
        }

        public void AddFlight(Flight flight)
        {
            _unitOfWork.FlightsRepository.Add(flight);
            _unitOfWork.Save();
        }
        /// <summary>
        /// Adds a new flight, calculates distance between departure and destination airport and needed fuel
        /// </summary>
        /// <param name="departureAirport">departure airport</param>
        /// <param name="destinationAirport">destination airport</param>
        /// <param name="aircraft">aircraft used fro the flight</param>
        public void AddFlight(Airport departureAirport, Airport destinationAirport, Aircraft aircraft)
        {
            var flight = new Flight()
            {
                Aircraft = aircraft,
                DepartureAirport = departureAirport,
                DestinationAirport = destinationAirport
            };

            var departureAddress = _geocoder.ReverseGeocode(new Location(departureAirport.Latitude, departureAirport.Longitude)).FirstOrDefault();
            var destinationAddress = _geocoder.ReverseGeocode(new Location(destinationAirport.Latitude, destinationAirport.Longitude)).FirstOrDefault();
            flight.Distance = departureAddress.DistanceBetween(destinationAddress, DistanceUnits.Kilometers).Value;
            flight.NeededFuel = ((flight.Distance / aircraft.GroundSpeed) * aircraft.FuelConsumptionPerHour) + aircraft.TakeOffEffort;
            AddFlight(flight);
        }

        public void DeleteFlight(Flight flight)
        {
            _unitOfWork.FlightsRepository.Delete(flight);
            _unitOfWork.Save();
        }

        public void EditFlight(Flight flight)
        {
            _unitOfWork.FlightsRepository.Edit(flight);
        }

        public IList<Flight> GetAllFlights()
        {
            return _unitOfWork.FlightsRepository.GetAll();
        }

        public Flight GetFlight(int flightId)
        {
            return _unitOfWork.FlightsRepository.FindById(flightId);
        }

        public void UpdateFlight(Flight flight, Airport departureAirport, Airport destinationAirport, Aircraft aircraft)
        {
            flight.Aircraft = aircraft;
            flight.DepartureAirport = departureAirport;
            flight.DestinationAirport = destinationAirport;
            var departureAddress = _geocoder.ReverseGeocode(new Location(departureAirport.Latitude, departureAirport.Longitude)).FirstOrDefault();
            var destinationAddress = _geocoder.ReverseGeocode(new Location(destinationAirport.Latitude, destinationAirport.Longitude)).FirstOrDefault();
            flight.Distance = departureAddress.DistanceBetween(destinationAddress, DistanceUnits.Kilometers).Value;
            flight.NeededFuel = ((flight.Distance / aircraft.GroundSpeed) * aircraft.FuelConsumptionPerHour) + aircraft.TakeOffEffort;
            _unitOfWork.Save();
        }
    }
}
