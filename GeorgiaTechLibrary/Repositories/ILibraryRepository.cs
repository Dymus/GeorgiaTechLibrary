using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface ILibraryRepository
    {
        //Task<IEnumerable<Library>> GetLibraries();
        Task<Library> GetLibrary(string libraryId);

        //Task<Library> CreateLibrary(Library library);
    }
}
