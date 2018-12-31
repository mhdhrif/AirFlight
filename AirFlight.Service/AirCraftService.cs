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
    public class AircraftService : IAircraftService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AircraftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAircraft(Aircraft aircraft)
        {
            _unitOfWork.AircraftsRepository.Add(aircraft);
            _unitOfWork.Save();
        }

        public void DeleteAircraft(Aircraft aircraft)
        {
            _unitOfWork.AircraftsRepository.Delete(aircraft);
            _unitOfWork.Save();
        }

        public void EditAircraft(Aircraft aircraft)
        {
            _unitOfWork.AircraftsRepository.Edit(aircraft);
            _unitOfWork.Save();
        }

        public Aircraft GetAircraft(int aircraftId)
        {
            return _unitOfWork.AircraftsRepository.FindById(aircraftId);
        }

        public IList<Aircraft> GetAllAircrafts()
        {
            return _unitOfWork.AircraftsRepository.GetAll();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
