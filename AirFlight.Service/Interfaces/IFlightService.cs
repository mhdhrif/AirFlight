using AirFlight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Service.Interfaces
{
    public interface IFlightService
    {
        IList<Flight> GetAllFlights();
        void AddFlight(Flight flight);
        void AddFlight(Airport departureAirport, Airport destinationAirport, Aircraft aircraft);
        void DeleteFlight(Flight flight);
        void EditFlight(Flight flight);
        Flight GetFlight(int flightId);
        void UpdateFlight(Flight flight, Airport departureAirport, Airport destinationAirport, Aircraft aircraft);
    }
}
