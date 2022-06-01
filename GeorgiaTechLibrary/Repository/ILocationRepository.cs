using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface ILocationRepository
    {
        //Task<IEnumerable<Location>> GetLocations();

        Task<Location> GetLocation(string locationId);

        //Task<Location> CreateLocation(Location location);

    }
}
