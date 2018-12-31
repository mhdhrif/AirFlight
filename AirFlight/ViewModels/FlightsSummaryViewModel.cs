using AirFlight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirFlight.ViewModels
{
    public class FlightsSummaryViewModel
    {
        public IEnumerable<Flight> Flights { get; set; }
        public IEnumerable<Airport> Airports { get; set; }
    }
}