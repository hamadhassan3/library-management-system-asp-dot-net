using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Service.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Lms.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
