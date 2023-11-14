using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;
using System.Linq.Expressions;

namespace HH.Lms.Data.Repository.EntityRepository;

public class BookRepository: GenericRepository<Book>
{
    public BookRepository()
    {

    }

    public BookRepository(LibraryDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Book>> findByTitleOrIsbn(string? title, string? isbn)
    {

        List<Expression<Func<Book, bool>>> predicates = new List<Expression<Func<Book, bool>>>();

        if (!string.IsNullOrWhiteSpace(title))
        {
            predicates.Add(book => book.Title.Contains(title));
        }
        if (!string.IsNullOrWhiteSpace(isbn))
        {
            predicates.Add(book => book.Isbn == isbn);
        }

        return await Find(predicates);
    }

    public async Task<bool> issueBookAsync(Book book, User user)
    {

        if (book == null || user == null)
        {
            return false;
        }

        if (book.UserId.HasValue)
        {
            return false;
        }

        book.UserId = user.Id;

        await this.SaveChangesAsync();

        return true;
    }

    public async Task<bool> returnBookAsync(Book book, User user)
    {

        if (book == null || user == null)
        {
            return false;
        }

        if (book.UserId != user.Id)
        {
            return false;
        }

        book.UserId = null;

        await this.SaveChangesAsync();

        return true;
    }
}
