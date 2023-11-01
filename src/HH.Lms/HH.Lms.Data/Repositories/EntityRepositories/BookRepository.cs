using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;

namespace HH.Lms.Data.Repository.EntityRepository
{
    public class BookRepository: GenericRepository<Book>
    {
        public BookRepository(LibraryDBContext dbContext) : base(dbContext)
        {
        }
    }
}
