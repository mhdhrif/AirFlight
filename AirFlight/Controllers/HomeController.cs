using AirFlight.Service.Interfaces;
using AirFlight.ViewModels;
using Geocoding;
using Geocoding.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AirFlight.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAircraftService _aircraftService;
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IGeocoder _geocoder;

        public HomeController(IAircraftService aircraftService, IAirportService airportService, IFlightService flightService, IGeocoder geocoder)
        {
            _aircraftService = aircraftService;
            _airportService = airportService;
            _flightService = flightService;
            _geocoder = geocoder;
        }

        public ActionResult Index()
        {
            try
            {
                List<SelectListItem> airCrafts = _aircraftService.GetAllAircrafts().Select(x =>
                                                new SelectListItem()
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.Model
                                                }).ToList();
                var flightViewModel = new FlightViewModel()
                {
                    Aircrafts = airCrafts
                };

                return View(flightViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EditFlight(int id)
        {
            try
            {
                List<SelectListItem> airCrafts = _aircraftService.GetAllAircrafts().Select(x =>
                                                new SelectListItem()
                                                {
                                                    Value = x.Id.ToString(),
                                                    Text = x.Model
                                                }).ToList();
                var flight = _flightService.GetFlight(id);
                var flightViewModel = new FlightViewModel()
                {
                    FlightId = id,
                    Aircrafts = airCrafts,
                    DepartureAirportGeoPoint = $"{flight.DepartureAirport.Latitude}|{flight.DepartureAirport.Longitude}",
                    DepartureAirportName = flight.DepartureAirport.Name,
                    DestinationAirportGeoPoint = $"{flight.DestinationAirport.Latitude}|{flight.DestinationAirport.Longitude}",
                    DestinationAirportName = flight.DestinationAirport.Name,
                    SelectedAircraftId = flight.Aircraft.Id
                };

                return View("Index", flightViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult FlightsList()
        {
            try
            {
                var flightSummaryViewModel = new FlightsSummaryViewModel()
                {
                    Flights = _flightService.GetAllFlights(),
                    Airports = _airportService.GetAllAirports()
                };

                return View(flightSummaryViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult FindAirport(string search)
        {
            try
            {
                IEnumerable<Address> addresses = _geocoder.Geocode(search);
                var airport = ((IEnumerable<GoogleAddress>)addresses).FirstOrDefault(x => x.Type == GoogleAddressType.Airport);
                List<SelectListItem> airports = new List<SelectListItem>();

                if (airport != null)
                {
                    string airportName = airport.FormattedAddress.IndexOf(",") > 0
                        ? airport.FormattedAddress.Remove(airport.FormattedAddress.IndexOf(","))
                        : airport.FormattedAddress;
                    airports.Add(new SelectListItem()
                    {
                        Text = airportName,
                        Value = $"{airport.Coordinates.Latitude}|{airport.Coordinates.Longitude}"
                    });
                }

                return new JsonResult()
                {
                    Data = airports.ToJSON(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult AddFlight(FlightViewModel flightViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<SelectListItem> airCrafts = _aircraftService.GetAllAircrafts().Select(x =>
                                                    new SelectListItem()
                                                    {
                                                        Value = x.Id.ToString(),
                                                        Text = x.Model
                                                    }).ToList();
                    flightViewModel.Aircrafts = airCrafts;

                    return View("Index", flightViewModel);
                }

                var departureAirport = _airportService.GetAirportBy(x => x.Name == flightViewModel.DepartureAirportName);

                if (departureAirport == null)
                {
                    departureAirport = _airportService.AddAirport(flightViewModel.DepartureAirportGeoPoint, flightViewModel.DepartureAirportName);
                }

                var destinationAirport = _airportService.GetAirportBy(x => x.Name == flightViewModel.DestinationAirportName);

                if (destinationAirport == null)
                {
                    destinationAirport = _airportService.AddAirport(flightViewModel.DestinationAirportGeoPoint, flightViewModel.DestinationAirportName);
                }

                var aircraft = _aircraftService.GetAircraft(flightViewModel.SelectedAircraftId);

                if (flightViewModel.FlightId == 0)
                {
                    _flightService.AddFlight(departureAirport, destinationAirport, aircraft);
                }
                else
                {
                    var flight = _flightService.GetFlight(flightViewModel.FlightId);
                    _flightService.UpdateFlight(flight, departureAirport, destinationAirport, aircraft);
                }

                return RedirectToAction("FlightsList");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DeleteFlight(int id)
        {
            try
            {
                var flight = _flightService.GetFlight(id);
                _flightService.DeleteFlight(flight);

                return RedirectToAction("FlightsList");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}