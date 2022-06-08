using Dapper;
using GeorgiaTechLibrary;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{ 
    public class LibraryRepository : ILibraryRepository
    {
        private readonly DapperContext _context;
        public LibraryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Library> GetLibrary(string libraryId)
        {
            var query = "SELECT l.library_id, l.name, loc.location_id, loc.post_code, pcc.city, loc.street, loc.street_num  FROM library l JOIN location loc ON loc.location_id=l.location_id JOIN post_code_city pcc ON pcc.post_code=loc.post_code WHERE l.library_id=@libraryId";
            
            using (var connection = _context.CreateConnection())
            {
                var library = await connection.QueryAsync<Library, Location, Library>(query, map: (library, location) =>
                {
                    library.Location = location;
                    return library;
                },
                param: new { libraryId },
                splitOn: "location_id");
                return library.FirstOrDefault();
            }
        }
    }
}
