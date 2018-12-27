using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Model
{
    public class Aircraft
    {
        public int Id { get; set; }
        public double FuelConsumptionPerHour { get; set; }
        public double TakeOffEffort { get; set; }
        public double GroundSpeed { get; set; }
    }
}
