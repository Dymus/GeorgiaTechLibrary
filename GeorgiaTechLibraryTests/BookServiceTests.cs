using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using GeorgiaTechLibrary.Controllers;
using Moq;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibraryTests
{
    public class BookServiceTests
    {
        private readonly BookController _bookController;
        private readonly LibraryService _libraryService;
        //private readonly Mock<IBookRepository> _bookRepoMock = new Mock<IBookRepository>();
        //private readonly BookRepository _bookRepository;
        private readonly Mock<ILibraryRepository> _libraryRepoMock = new Mock<ILibraryRepository>();
        
        public BookServiceTests(IBookRepository bookRepository)
        {
            _libraryService = new LibraryService(_libraryRepoMock.Object);
        }


        //[Fact]
        //public async Task CreateReadBook()
        //{
        //    var isbn = "01234567899";
        //    //Arrange
        //    BookDTO newBook = new BookDTO();
        //    newBook.ISBN = isbn;
        //    newBook.Title = "New Book";
        //    newBook.Description = "asdasdjasgjhfweufghhdsfjsdakdakcnxzbcnzvyqtewqwpqslmdskvsdvs";
        //    newBook.Volumes = new List<VolumeDTO>();
        //     VolumeDTO volume1 = new VolumeDTO();
        //    volume1.Isbn = isbn;
        //    volume1.Is_available = true;
        //    volume1.Library_id = 2;
        //    VolumeDTO volume2 = new VolumeDTO();
        //    volume2.Isbn = isbn;
        //    volume2.Is_available = false;
        //    volume2.Library_id = 3;
        //    VolumeDTO volume3 = new VolumeDTO();
        //    volume3.Isbn = isbn;
        //    volume3.Is_available = true;
        //    volume3.Library_id = 2;
        //    newBook.Volumes.Add(volume1);
        //    newBook.Volumes.Add(volume2);
        //    newBook.Volumes.Add(volume3);
        //    //Act
        //    var result = await _bookController.CreateBook(newBook);
        //    //Assert

        //    //Act
        //    var book = await _bookController.GetBookIncludeVolumes(isbn);
        //    Assert.NotNull(book);
        //    Assert.Equal(newBook.Title, book.Title);
        //    Assert.Equal(newBook.Description, book.Description);
        //    Assert.True(newBook.Volumes.Count == 3);
        //}
    }
}
