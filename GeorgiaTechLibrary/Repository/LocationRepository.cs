using Dapper;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DapperContext _context;

        public  LocationRepository(DapperContext context)
        {
            _context = context;
        }
        //public async Task<IEnumerable<Location>> GetLocations()
        //{
        //    var query = "SELECT TOP(100) * FROM location";
        //    using (var connection = _context.CreateConnection())
        //    {
        //        //var locations = await connection.QueryAsync<Location>(query);
        //        //return locations.ToList();
        //    }
        //    return null;
        //}
        public async Task<Location> GetLocation(string locationId)
        {
            var query = "SELECT location_id, post_code, street, street_num FROM location WHERE location_id=@locationId";
            //var query = "SELECT location_id LocationId, post_code PostCode, street, street_num streetNum FROM location WHERE location_id=@locationId";
            using (var connection = _context.CreateConnection())
            {
                
                var location = await connection.QuerySingleOrDefaultAsync<Location>(query, new { locationId });
                return location;
            }

        }

       

       
    }
}
