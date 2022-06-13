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

        public async Task<Volume> CreateVolume(VolumeDTO volume)
        {
            var query = "INSERT INTO volume (isbn, is_available, library_id) OUTPUT inserted.volume_id, inserted.isbn, inserted.is_available, inserted.library_id VALUES(@ISBN, @is_available, @library_id)";
            using (var connection = _context.CreateConnection())
            {
                var insertedVolume = await connection.QuerySingleAsync<Volume>(query, new { volume.Isbn, volume.Is_available, volume.Library_id });
                return insertedVolume;
            }
        }

        public async Task<Volume> GetVolume(string volume_id)
        {
            var query = "SELECT v.volume_id, v.isbn, v.is_available, v.library_id, l.library_id, l.name, l.location_id, loc.location_id, loc.post_code, pcc.city, loc.street, loc.street_num FROM volume v JOIN library l ON v.library_id=l.library_id JOIN location loc ON l.location_id=loc.location_id JOIN post_code_city pcc ON loc.post_code=pcc.post_code WHERE volume_id=@volume_id";
            using (var connection = _context.CreateConnection())
            {
                var volume = await connection.QueryAsync<Volume, Library, Location, Volume>(query, map:(volume, library, location) =>
                {
                    volume.Library = library;
                    volume.Library.Location = location;
                    return volume;
                },
                new { volume_id },
                splitOn: "library_id, location_id");
                return volume.First();
            }
        }

        //returns all volumes for a book
        public async Task<IEnumerable<Volume>> GetAllVolumes(string ISBN)
        {
            var query = "SELECT v.volume_id, v.isbn, v.is_available, v.library_id, l.library_id, l.name, l.location_id, loc.location_id, loc.post_code, pcc.city, loc.street, loc.street_num FROM volume v JOIN library l ON v.library_id=l.library_id JOIN location loc ON l.location_id=loc.location_id JOIN post_code_city pcc ON loc.post_code=pcc.post_code WHERE isbn=@ISBN";

            using(var connection = _context.CreateConnection())
            {
                var volumes = await connection.QueryAsync<Volume, Library, Location, Volume>
                    (query, map:(volume, library, location) =>
                    {
                        volume.Library = library;
                        volume.Library.Location = location;
                        return volume;
                    },
                    new { ISBN },
                    splitOn: "library_id, location_id");
                return volumes.ToList();
            }
        }

        //returns all available volumes for a book
        public async Task<IEnumerable<Volume>> GetAvailableVolumes(string ISBN)
        {
            var query = "SELECT v.volume_id, v.isbn, v.is_available, v.library_id, l.library_id, l.name, l.location_id, loc.location_id, loc.post_code, pcc.city, loc.street, loc.street_num FROM volume v JOIN library l ON v.library_id=l.library_id JOIN location loc ON l.location_id=loc.location_id JOIN post_code_city pcc ON loc.post_code=pcc.post_code WHERE isbn=@ISBN AND is_available=1";
            using (var connection = _context.CreateConnection())
            {
                var volumes = await connection.QueryAsync<Volume, Library, Location, Volume>
                    (query, map:(volume, library, location) =>
                    {
                        volume.Library = library;
                        volume.Library.Location = location;
                        return volume;
                    },
                    new { ISBN },
                    splitOn: "library_id, location_id");
                return volumes.ToList();
            }
        }


        public async Task<bool> SetVolumeToUnavailable(string volume_id)
        {
            var query = "UPDATE volume SET is_available=0 WHERE volume_id=@volume_id";

            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new {volume_id});
                if (affectedRows > 0) return true;
                else return false;
            }
        }

        public async Task<bool> IsVolumeAvailable(string volume_id)
        {
            var query = "SELECT is_available from volume where volume_id=@volume_id";
            using (var connection = _context.CreateConnection())
            {
                var available = await connection.QuerySingleOrDefaultAsync<bool>(query, new { volume_id });
                return available;
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
