using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AirFlight.Test
{
    public class FlightsDoohickeyTests
    {
        [Fact]
        public void FlightHasDepatureAirportAndDestinationAirport()
        {
            var repo = new Mock<IFlightRepository>();
            var service = new Mock<IFlightService>();
            var flight = new Mock<Flight>();
            //FlightsDoohickey thing = new FlightsDoohickey(repo, service);
            //thing.AddNewFlight(flight);

            //flight.Verify(x => x.)
            
        }
        //Adding Flights
        //a flight has de departure airport and a destination airport
        //Each flight has a calculated distance
        //Each Flight needs a quantity of Fuel
        //Each flight is made by an AirCraft
        //Each airport has gps positions
    }

    public interface IFlightRepository
    {
    }
    public interface IFlightService
    {
    }
    public class Flight
    {
    }
    public class Airport
    {
    }
    public class AirCraft
    {
    }
}
