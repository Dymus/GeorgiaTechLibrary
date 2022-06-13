using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookSubjectRepository _bookSubjectRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ISubjectRepository subjectRepository, IBookAuthorRepository bookAuthorRepository, IBookSubjectRepository bookSubjectRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _subjectRepository = subjectRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookSubjectRepository = bookSubjectRepository;
        }

        //public Task<Book> GetBookIncludeVolumes(string ISBN) => _bookRepository.GetBookIncludeVolumes(ISBN);

        public Task<Book> GetBook(string ISBN) => _bookRepository.GetBook(ISBN);

        public async Task<Book> CreateBook(Book book) 
        {
            var insertedBook = await _bookRepository.CreateBook(book);
            insertedBook.Authors = new List<Author>();
            insertedBook.Subjects = new List<Subject>();
            foreach (Author author in book.Authors)
            {
                var result = await _authorRepository.GetAuthor(author.F_name, author.L_name);
                if(result == null)
                {
                    result = await _authorRepository.CreateAuthor(author.F_name, author.L_name);
                }
                await _bookAuthorRepository.AttachAuthorToBook(book.ISBN, result);
                insertedBook.Authors.Add(result);
            }
            foreach (Subject subject in book.Subjects)
            {
                var result = await _subjectRepository.GetSubject(subject.Name);
                if (result == null)
                {
                    result = await _subjectRepository.CreateSubject(subject.Name);
                }
                await _bookSubjectRepository.AttachSubjectToBook(book.ISBN, result);
                insertedBook.Subjects.Add(result);
            }
            return insertedBook;
        }
    }
}
