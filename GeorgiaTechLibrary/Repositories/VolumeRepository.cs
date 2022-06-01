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
            var query = "SELECT * FROM volume WHERE volume_id=@volumeId";
            using (var connection = _context.CreateConnection())
            {
                var volume = await connection.QuerySingleOrDefaultAsync<Volume>(query, new { volumeId });
                return volume;
            }
        }

        public async Task<IEnumerable<Volume>> GetVolumes(string ISBN)
        {
            var query = "SELECT * FROM volume WHERE isbn=@ISBN";

            using(var connection = _context.CreateConnection())
            {
                var volumes = await connection.QueryAsync<Volume>(query, new { ISBN });
                return volumes.ToList();
            }
        }

        public async Task<IEnumerable<Volume>> GetAvailableVolumes(string ISBN)
        {
            var query = "SELECT * FROM volume WHERE isbn=@ISBN AND is_available=1";

            using (var connection = _context.CreateConnection())
            {
                var volumes = await connection.QueryAsync<Volume>(query, new { ISBN });
                return volumes.ToList();
            }
        }

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
