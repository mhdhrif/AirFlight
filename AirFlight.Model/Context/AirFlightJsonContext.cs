using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Model.Context
{
    public class AirFlightJsonContext
    {
        private readonly string _storeLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

        public IList<Flight> Flights { get; set; }
        public IList<Aircraft> Aircrafts { get; set; }
        public IList<Airport> Airports { get; set; }

        public AirFlightJsonContext()
        {
            Flights = JsonConvert.DeserializeObject<List<Flight>>(File.ReadAllText($@"{_storeLocation}\flights.json"));
            Aircrafts = JsonConvert.DeserializeObject<List<Aircraft>>(File.ReadAllText($@"{_storeLocation}\aircrafts.json"));
            Airports = JsonConvert.DeserializeObject<List<Airport>>(File.ReadAllText($@"{_storeLocation}\airports.json"));
        }

        public void SaveChanges()
        {
            File.WriteAllText($@"{_storeLocation}\flights.json", JsonConvert.SerializeObject(Flights));
            File.WriteAllText($@"{_storeLocation}\aircrafts.json", JsonConvert.SerializeObject(Aircrafts));
            File.WriteAllText($@"{_storeLocation}\airports.json", JsonConvert.SerializeObject(Airports));
        }

        public void Dispose()
        {
            Flights.Clear();
            Aircrafts.Clear();
            Airports.Clear();
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
