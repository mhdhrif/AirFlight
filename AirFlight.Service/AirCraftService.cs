using AirFlight.Model;
using AirFlight.Repository.Interfaces;
using AirFlight.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Service
{
    public class AirCraftService : IAirCraftService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AirCraftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IList<Aircraft> GetAllAirCrafts()
        {
            return _unitOfWork.AirCraftRepository.GetAll();
        }
    }
}
