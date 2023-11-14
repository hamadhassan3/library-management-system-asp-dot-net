using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Library;
using HH.Lms.Service.Library.Dto;
using Moq;
using Xunit;

namespace HH.Lms.UnitTest.ServiceTests
{
    public class BookServiceTests
    {
        [Fact]
        public async Task AddAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<BookRepository>();
            var bookMock = new Mock<Book>();

            var bookDto = new BookDto();

            repositoryMock.Setup((r) => r.Add(It.IsAny<Book>())).Callback(() => { 
                // Does nothing
            });
            mapperMock.Setup((s) => s.Map<Book>(bookDto)).Returns(bookMock.Object);
            var bookService = new BookService(repositoryMock.Object, mapperMock.Object);

            var result = await bookService.AddAsync(bookDto);

            Assert.True(result.Success);
            repositoryMock.Verify(r => r.Add(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<BookRepository>();
            var bookMock = new Mock<Book>();

            repositoryMock.Setup((r) => r.Update(It.IsAny<Book>())).Callback(() => {
                // Does nothing
            });
            mapperMock.Setup((s) => s.Map<Book>(It.IsAny<BookDto>())).Returns(bookMock.Object);
            var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
            var bookDto = new BookDto();

            var result = await bookService.UpdateAsync(bookDto);

            Assert.True(result.Success);
            Assert.Equal(bookDto, result.Data);
            repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<BookRepository>();
            var bookMock = new Mock<Book>();

            repositoryMock.Setup((r) => r.Delete(It.IsAny<Book>())).Callback(() => {
                // Does nothing
            });
            mapperMock.Setup((s) => s.Map<Book>(It.IsAny<BookDto>())).Returns(bookMock.Object);
            var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
            var bookId = 1;

            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Book());

            await bookService.DeleteAsync(bookId);

        }

        [Fact]
        public async Task GetAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<BookRepository>();
            var bookMock = new Mock<Book>();
            var bookDto = new Mock<BookDto>();

            mapperMock.Setup((s) => s.Map<BookDto>(It.IsAny<Book>())).Returns(bookDto.Object);
            var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
            var bookId = 1;

            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(bookMock.Object);

            var result = await bookService.GetAsync(bookId);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            mapperMock.Verify(m => m.Map<BookDto>(It.IsAny<Book>()), Times.Once);
        }
    }
}
