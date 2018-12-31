using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        public Aircraft Aircraft { get; set; }
        public double Distance { get; set; }
        public double NeededFuel { get; set; }
    }
}
