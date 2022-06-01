using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Task<Location> GetLocation(string locationId) => _locationRepository.GetLocation(locationId);
    }
}
