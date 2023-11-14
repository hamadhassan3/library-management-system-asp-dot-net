using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Base;
using HH.Lms.Service.Library.Dto;

namespace HH.Lms.Service.Library;

public class BookService : BaseService<Book, BookDto>
{
    private BookRepository repository { get; }

    private UserRepository userRepository { get; }

    public BookService(
        BookRepository repository,
        UserRepository userRepository,
        IMapper mapper)
        : base(repository, mapper)
    {
        this.userRepository = userRepository;
        this.repository = repository;
    }

    public async Task<ServiceResult<IEnumerable<BookDto>>> findByTitleOrIsbnAsync(string? title, string? isbn)
    {
        IEnumerable<Book> books = await repository.findByTitleOrIsbn(title, isbn);
        return new ServiceResult<IEnumerable<BookDto>>
        {
            Success = true,
            Errors = new List<string>(),
            Data = Mapper.Map<IEnumerable<BookDto>>(books)
        };
    }

    public async Task<ServiceResult<BookDto>> issueBookAsync(int bookId, int userId)
    {
        Book book = await repository.GetByIdAsync(bookId);
        User user = await userRepository.GetByIdAsync(userId);

        if (user == null) {
            return Failure("A user with this id does not exist!");
        }
        else if (book == null)
        {
            return Failure("A book with this id does not exist!");
        }
        else if (book.UserId.HasValue)
        {
            return Failure("This book already has a user associated with it!");
        }

        bool result = await repository.issueBookAsync(book, user);

        if (result)
        {
            return Success(Mapper.Map<BookDto>(await repository.GetByIdAsync(bookId)));
        }
        else
        {
            return Failure("Failed to issue with unknown error!");
        }


    }

    public async Task<ServiceResult<BookDto>> returnBookAsync(int bookId, int userId)
    {
        Book book = await repository.GetByIdAsync(bookId);
        User user = await userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return Failure("A user with this id does not exist!");
        }
        else if (book == null)
        {
            return Failure("A book with this id does not exist!");
        }
        else if (book.UserId != userId)
        {
            return Failure("This book is not associated with this user!");
        }

        bool result = await repository.returnBookAsync(book, user);

        if (result)
        {

            return Success(Mapper.Map<BookDto>(await repository.GetByIdAsync(bookId)));
        }
        else
        {
            return Failure("Failed to return with unknown error!");
        }
    }
}
