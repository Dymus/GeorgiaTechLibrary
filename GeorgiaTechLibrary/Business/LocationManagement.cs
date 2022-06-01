using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class LocationManagement
    {
        private readonly ILocationRepository _locationRepository;
        public LocationManagement(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<Location> GetLocation(string locationId) => _locationRepository.GetLocation(locationId);
    }
}
