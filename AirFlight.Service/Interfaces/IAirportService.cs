using AirFlight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Service.Interfaces
{
    public interface IAirportService
    {
        IList<Airport> GetAllAirports();
        void AddAirport(Airport airport);
        Airport AddAirport(string airportGeoPoint, string airportName);
        void DeleteAirport(Airport airport);
        void EditAirport(Airport airport);
        Airport GetAirport(int airportId);
        Airport GetAirportBy(Func<Airport, bool> predicate);
    }
}
