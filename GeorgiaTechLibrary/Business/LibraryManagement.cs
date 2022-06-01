using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class LibraryManagement
    {
        private readonly ILibraryRepository _libraryRepository;
        public LibraryManagement(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        public Task<Library> GetLibrary(string libraryId) => _libraryRepository.GetLibrary(libraryId);
    }
}
