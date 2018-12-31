using Foolproof;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirFlight.ViewModels
{
    public class FlightViewModel
    {
        public int FlightId { get; set; }
        [NotEqualTo("DestinationAirportGeoPoint", ErrorMessage = "Please select different airports for Departure and destination")]
        public string DepartureAirportGeoPoint { get; set; }
        public string DepartureAirportName { get; set; }
        public string DestinationAirportGeoPoint { get; set; }
        public string DestinationAirportName { get; set; }
        public int SelectedAircraftId { get; set; }
        public IEnumerable<SelectListItem> Aircrafts { get; set; } 
    }
}