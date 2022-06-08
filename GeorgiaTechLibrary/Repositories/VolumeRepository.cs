using Dapper;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class VolumeRepository : IVolumeRepository
    {
        private readonly DapperContext _context;
        public VolumeRepository(DapperContext context)
        {
            _context = context;
        }

        public Task<Volume> CreateVolume(Volume volume)
        {
            throw new NotImplementedException();
        }

        public async Task<Volume> GetVolume(string volumeId)
        {
            var query = "SELECT v.volume_id, v.isbn, v.is_available, v.library_id, l.name, l.location_id, loc.post_code, pcc.city, loc.street, loc.street_num FROM volume v JOIN library l ON v.library_id = l.library_id JOIN location loc ON l.location_id = loc.location_id JOIN post_code_city pcc ON loc.post_code = pcc.post_code WHERE volume_id = @volumeId";
            using (var connection = _context.CreateConnection())
            {
                var volume = await connection.QueryAsync<Volume, Library, Volume>(query, map:(volume, library) =>
                {
                    volume.Library = library;
                    return volume;
                },
                new { volumeId },
                splitOn: "library_id");
                return volume.First();
            }
        }

        //returns all volumes for a book
        public async Task<IEnumerable<Volume>> GetAllVolumes(string ISBN)
        {
            var query = "SELECT v.volume_id, v.isbn, v.is_available, v.library_id, l.name, l.location_id, loc.post_code, pcc.city, loc.street, loc.street_num FROM volume v JOIN library l ON v.library_id=l.library_id JOIN location loc ON l.location_id=loc.location_id JOIN post_code_city pcc ON loc.post_code=pcc.post_code WHERE isbn=@ISBN";

            using(var connection = _context.CreateConnection())
            {
                var volumes = await connection.QueryAsync<Volume, Library, Volume>
                    (query, map:(volume, library) =>
                    {
                        volume.Library = library;
                        return volume;
                    },
                    new { ISBN },
                    splitOn: "library_id");
                return volumes.ToList();
            }
        }

        //returns all available volumes for a book
        public async Task<IEnumerable<Volume>> GetAvailableVolumes(string ISBN)
        {
            var query = "SELECT v.volume_id, v.isbn, v.is_available, v.library_id, l.name, l.location_id, loc.post_code, pcc.city, loc.street, loc.street_num FROM volume v JOIN library l ON v.library_id=l.library_id JOIN location loc ON l.location_id=loc.location_id JOIN post_code_city pcc ON loc.post_code=pcc.post_code WHERE isbn=@ISBN AND is_available=1";

            using (var connection = _context.CreateConnection())
            {
                var volumes = await connection.QueryAsync<Volume, Library, Volume>
                    (query, map:(volume, library) =>
                    {
                        volume.Library = library;
                        return volume;
                    },
                    new { ISBN },
                    splitOn: "library_id");
                return volumes.ToList();
            }
        }

        //returns a single available volume for a book
        //TODO: still does not work with a stored procedure
        public async Task<Volume> GetAvailableVolume(string ISBN)
        {
            var query = "EXEC GetAvailableVolume @ISBN";

            using (var connection = _context.CreateConnection())
            {
                var volume = await connection.QuerySingleOrDefaultAsync<Volume>(query, new { ISBN });
                return volume;
            }
        }
    }
}
