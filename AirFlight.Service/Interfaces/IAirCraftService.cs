using AirFlight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Service.Interfaces
{
    public interface IAircraftService : IDisposable
    {
        IList<Aircraft> GetAllAircrafts();
        void AddAircraft(Aircraft aircraft);
        void DeleteAircraft(Aircraft aircraft);
        void EditAircraft(Aircraft aircraft);
        Aircraft GetAircraft(int aircraftId);
    }
}
