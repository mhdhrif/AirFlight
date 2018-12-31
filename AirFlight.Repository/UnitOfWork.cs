using AirFlight.Model;
using AirFlight.Model.Context;
using AirFlight.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirFlightJsonContext _context;

        public UnitOfWork()
        {
            _context = new AirFlightJsonContext();
            AircraftsRepository = new GenericRepository<Aircraft>(_context);
            AirportsRepository = new GenericRepository<Airport>(_context);
            FlightsRepository = new GenericRepository<Flight>(_context);
        }

        public IGenericRepository<Aircraft> AircraftsRepository { get; private set; }

        public IGenericRepository<Airport> AirportsRepository { get; private set; }

        public IGenericRepository<Flight> FlightsRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
