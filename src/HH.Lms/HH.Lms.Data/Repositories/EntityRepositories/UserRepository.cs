using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HH.Lms.Data.Repository.EntityRepository;

public class UserRepository: GenericRepository<User>
{
    public UserRepository() : base() { }
    public UserRepository(LibraryDBContext baseDBContext) : base(baseDBContext) { 
    }

    public async Task<User> loadBooks(User user)
    {
        await _dbContext.Entry(user)
            .Collection(u => u.Books)
            .LoadAsync();

        return user;
    }
}
