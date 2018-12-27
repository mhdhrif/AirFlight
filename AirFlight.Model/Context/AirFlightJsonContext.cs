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

        public IList<T> FindBy<T>(Func<T, bool> predicate)
        {
            switch (typeof(T).Name)
            {
                case "Flight":
                    return ((IList<T>)Flights).Where(predicate).ToList();
                case "Aircraft":
                    return ((IList<T>)Aircrafts).Where(predicate).ToList();
                case "Airport":
                    return ((IList<T>)Airports).Where(predicate).ToList();
                default:
                    return null;
            }
        }

        public T Find<T>(int id)
        {
            switch (typeof(T).Name)
            {
                case "Flight":
                    return (T) Convert.ChangeType(Flights.FirstOrDefault(f => f.Id == id), typeof(T));
                case "Aircraft":
                    return (T)Convert.ChangeType(Aircrafts.FirstOrDefault(f => f.Id == id), typeof(T));
                case "Airport":
                    return (T)Convert.ChangeType(Airports.FirstOrDefault(f => f.Id == id), typeof(T));
                default:
                    return default(T);
            }
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
