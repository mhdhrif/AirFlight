using AirFlight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Aircraft> AircraftsRepository { get; }
        IGenericRepository<Airport> AirportsRepository { get; }
        IGenericRepository<Flight> FlightsRepository { get; }
        void Save();
    }
}
