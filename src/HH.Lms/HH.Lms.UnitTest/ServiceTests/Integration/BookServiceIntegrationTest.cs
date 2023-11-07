using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Library;
using Xunit;
using AutoMapper;
using Moq;
using HH.Lms.Service.Library.Interfaces;
using HH.Lms.Service.Library.Dto;
using HH.Lms.Service.Base;

namespace HH.Lms.UnitTest.ServiceTests.Integration
{
    public class BookServiceIntegrationTest
    {
        private readonly DbContextOptions<LibraryDBContext> dbContextOptions;
        private readonly LibraryDBContext dbContext;
        private readonly BookDto testBook1;
        private readonly BookDto testBook2;
        private readonly BookDto newBook;
        private readonly IMapper mapper;
        private readonly BookService bookService;
        private readonly BookDto updatedBook;

        public BookServiceIntegrationTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LibraryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new LibraryDBContext(dbContextOptions);

            testBook1 = new BookDto
            {
                Id = 1,
                Author = "Test Author1",
                Title = "Test Book1",
                Isbn = "Test ISBN1",
                Description = "Description1"

            };

            testBook2 = new BookDto
            {
                Id = 2,
                Author = "Test Author2",
                Title = "Test Book2",
                Isbn = "Test ISBN2",
                Description = "Description2"

            };

            newBook = new BookDto
            {
                Id = 10,
                Author = "New Author",
                Title = "New Book",
                Isbn = "New ISBN",
                Description = "New Description"
            };
            var newBookObj = new Book
            {
                Id = 10,
                Author = "New Author",
                Title = "New Book",
                Isbn = "New ISBN",
                Description = "New Description"
            };

            updatedBook = new BookDto
            {
                Id = testBook1.Id,
                Author = "Updated Author",
                Title = "Updated Book",
                Isbn = "Updated ISBN",
                Description = "Updated Description"
            };

            var updatedBookObj = new Book
            {
                Id = testBook1.Id,
                Author = "Updated Author",
                Title = "Updated Book",
                Isbn = "Updated ISBN",
                Description = "Updated Description"
            };

            var books = new List<Book>
            {
                new Book
                    {
                        Id = 1,
                        Author = "Test Author1",
                        Title = "Test Book1",
                        Isbn = "Test ISBN1",
                        Description = "Description1"
                    },
                new Book
                    {
                        Id = 2,
                        Author = "Test Author2",
                        Title = "Test Book2",
                        Isbn = "Test ISBN2",
                        Description = "Description2"
                    }
            };

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup((m) => m.Map<BookDto>(books[0])).Returns(testBook1);
            mapperMock.Setup((m) => m.Map<BookDto>(books[1])).Returns(testBook2);
            mapperMock.Setup((m) => m.Map<BookDto>(newBookObj)).Returns(newBook);
            mapperMock.Setup((m) => m.Map<BookDto>(updatedBookObj)).Returns(updatedBook);

            mapperMock.Setup((m) => m.Map<Book>(testBook1)).Returns(books[0]);

            mapperMock.Setup((m) => m.Map<Book>(testBook2)).Returns(books[1]);

            mapperMock.Setup((m) => m.Map<Book>(newBook)).Returns(newBookObj);
            mapperMock.Setup((m) => m.Map<Book>(updatedBook)).Returns(updatedBookObj);


            mapperMock.Setup((m) => m.Map<IEnumerable<BookDto>>(It.IsAny<IEnumerable<Book>>())).Returns(new List<BookDto> { testBook1, testBook2 }.AsEnumerable());

            mapper = mapperMock.Object;

            bookService = new BookService(new BookRepository(dbContext), mapper);
        }

        [Fact]
        public async void GetAllBooks_ShouldReturnAllBooks()
        {
            // Adding books
            await bookService.AddAsync(testBook1);
            await bookService.AddAsync(testBook2);

            var result = await bookService.GetAllAsync();

            var ls = result.Data.ToList();

            Assert.NotNull(result);
            Assert.IsType<List<BookDto>>(ls);
            Assert.Equal(2, ls.Count);
            Assert.Contains(ls, b => b.Id == testBook1.Id);
            Assert.Contains(ls, b => b.Id == testBook2.Id);

            // Cleaning up
            await bookService.DeleteAsync(testBook1.Id);
            await bookService.DeleteAsync(testBook2.Id);
        }

        [Fact]
        public async void GetBookById_ShouldReturnCorrectBook()
        {
            await bookService.AddAsync(testBook1);

            var result = bookService.GetAsync(testBook1.Id);

            Assert.NotNull(result);
            Assert.Equal(testBook1.Id, result.Id);

            await bookService.DeleteAsync(testBook1.Id);
        }

        [Fact]
        public async void AddBook_ShouldAddNewBook()
        {

            var addedBook = await bookService.AddAsync(newBook);

            Assert.NotNull(addedBook);
            Assert.Equal(newBook.Author, addedBook.Data.Author);
            Assert.Equal(newBook.Title, addedBook.Data.Title);

            var retrievedBook = dbContext.Books.Find(addedBook.Data.Id);
            Assert.NotNull(retrievedBook);
            Assert.Equal(newBook.Author, retrievedBook.Author);
            Assert.Equal(newBook.Title, retrievedBook.Title);

            await bookService.DeleteAsync(addedBook.Data.Id);
        }

        [Fact]
        public async void UpdateBook_ShouldUpdateExistingBook()
        {
            var res = await bookService.AddAsync(testBook1);

            BookDto addedBook = res.Data;

            addedBook.Author = updatedBook.Author;
            addedBook.Title = updatedBook.Title;
            addedBook.Description = updatedBook.Description;

            var result = await bookService.UpdateAsync(addedBook);

            Assert.NotNull(result);
            Assert.Equal(updatedBook.Author, result.Data.Author);
            Assert.Equal(updatedBook.Title, result.Data.Title);

            var retrievedBook = await bookService.GetAsync(result.Data.Id);
            Assert.NotNull(retrievedBook);
            Assert.Equal(updatedBook.Author, retrievedBook.Data.Author);
            Assert.Equal(updatedBook.Title, retrievedBook.Data.Title);

            await bookService.DeleteAsync(retrievedBook.Data.Id);
        }
    }
}
