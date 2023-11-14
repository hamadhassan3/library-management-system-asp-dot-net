using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Base;
using HH.Lms.Service.Library.Dto;

namespace HH.Lms.Service.Library
{
    public class UserService : BaseService<User, UserDto>
    {
        private UserRepository repository { get; }

        public UserService(
            UserRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            this.repository = repository;
        }

        public async Task<ServiceResult<UserDto>> loadBooks(int userId)
        {
            User user = await repository.GetByIdAsync(userId);

            if (user == null)
            {
                return Failure("A user with this id does not exist!");
            }

            await repository.loadBooks(user);

            return Success(Mapper.Map<UserDto>(user));
        }
    }
}
