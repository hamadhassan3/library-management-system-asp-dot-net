using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;

namespace HH.Lms.Data.Repository.EntityRepository;

public class UserRepository: GenericRepository<User>
{
    public UserRepository(LibraryDBContext baseDBContext) : base(baseDBContext) { 
    }
}
