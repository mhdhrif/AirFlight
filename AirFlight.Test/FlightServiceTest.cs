using AirFlight.Model;
using AirFlight.Repository.Interfaces;
using AirFlight.Service;
using Geocoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace AirFlight.Test
{
    public class FlightServiceTest
    {
        [TestMethod]
        public void CanAddFlightService()
        {
            List<Flight> flights = new List<Flight>()
            {
                new Flight()
                {
                    Aircraft = new Aircraft()
                    {
                        FuelConsumptionPerHour = 250,
                        GroundSpeed = 650,
                        Id = 1,
                        Model = "Boeing 747",
                        TakeOffEffort = 200
                    },
                    Id = 1,
                    Distance = 500,
                    NeededFuel = 560,
                    DepartureAirport = new Airport()
                    {
                        Id = 1,
                        Latitude=25.5885,
                        Longitude = 10.5589,
                        Name = "Orly Airport"
                    },
                    DestinationAirport = new Airport()
                    {
                        Id = 2,
                        Latitude = 25.69985,
                        Longitude = 12.2558,
                        Name = "Santorini airport"
                    }
                }
            };

            var flightRepo = new Mock<IGenericRepository<Flight>>();
            flightRepo.Setup(r => r.GetAll()).Returns(flights);
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(u => u.FlightsRepository).Returns(flightRepo.Object);
            var geocoder = new Mock<IGeocoder>();
            var flightService = new FlightService(uow.Object, geocoder.Object);

            flightService.AddFlight(new Airport()
            {
                Id = 3,
                Latitude = 19.36699,
                Longitude = 13.6699,
                Name = "My airport"
            },
            new Airport()
            {
                Id = 4,
                Latitude = 15.36699,
                Longitude = 14.6699,
                Name = "My airport 2"
            },
            new Aircraft()
            {
                FuelConsumptionPerHour = 250,
                GroundSpeed = 650,
                Id = 1,
                Model = "Airbus A320",
                TakeOffEffort = 200
            });

            var result = flightService.GetAllFlights();
            var newFlight = result.FirstOrDefault(f => f.Id == 2);

            Assert.AreEqual(flights.Count, result.Count);
            Assert.IsNotNull(newFlight);
            Assert.IsNotNull(newFlight.DepartureAirport);
            Assert.IsNotNull(newFlight.DestinationAirport);
        }
    }
}
