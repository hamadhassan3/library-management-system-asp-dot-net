using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Base;
using HH.Lms.Service.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
