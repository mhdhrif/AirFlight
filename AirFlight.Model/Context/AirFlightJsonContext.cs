using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Model.Context
{
    public class AirFlightJsonContext
    {
        public IList<Flight> Flights { get; set; }
        public IList<Aircraft> Aircrafts { get; set; }
        public IList<Airport> Airports { get; set; }

        public AirFlightJsonContext(string storeLocation)
        {
            Flights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText($@"{storeLocation}\flights.json"));
            Aircrafts = JsonConvert.DeserializeObject<List<Aircraft>>(File.ReadAllText($@"{storeLocation}\aircrafts.json"));
            Airports = JsonConvert.DeserializeObject<List<Airport>>(File.ReadAllText($@"{storeLocation}\airports.json"));
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IList<T> GetListByType<T>()
        {
            switch (typeof(T).Name)
            {
                case "Flight":
                    return (IList<T>) Flights;
                case "Aircraft":
                    return (IList<T>) Aircrafts;
                case "Airport":
                    return (IList<T>) Airports;
                default:
                    return null;
            }
        }
    }
}
