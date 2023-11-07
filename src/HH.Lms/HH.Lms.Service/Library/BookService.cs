using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Base;
using HH.Lms.Service.Library.Dto;
using Microsoft.Extensions.Logging;

namespace HH.Lms.Service.Library
{
    public class BookService : BaseService<Book, BookDto>
    {
        private BookRepository repository { get; }

        public BookService(
            BookRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            this.repository = repository;
        }
    }
}
