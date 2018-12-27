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

        public UnitOfWork(/*AirFlightJsonContext context*/string storeLocation)
        {
            _context = new AirFlightJsonContext(storeLocation);
            AirCraftRepository = new GenericRepository<Aircraft>(_context);
        }

        public IGenericRepository<Aircraft> AirCraftRepository { get; private set; }
        
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
