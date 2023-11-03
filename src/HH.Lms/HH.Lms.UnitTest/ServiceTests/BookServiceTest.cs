using AutoMapper;
using Folio3.Sbp.Service;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository;
using HH.Lms.Service.Base;
using HH.Lms.Service.Library;
using HH.Lms.Service.Library.Dto;
using Moq;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HH.Lms.UnitTest.ServiceTests
{
    //public class BookServiceTests
    //{
    //    [Fact]
    //    public async Task AddAsync_ShouldReturnSuccessResult()
    //    {
    //        // Arrange
    //        var mapperMock = new Mock<IMapper>();
    //        var repositoryMock = new Mock<GenericRepository<Book>>();

    //        var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
    //        var bookDto = new BookDto(); // Assuming BookDto is a class implementing IDto

    //        // Act
    //        var result = bookService.AddAsync(bookDto);

    //        // Assert
    //        Assert.True(result.Success);
    //        Assert.Equal(bookDto, result.Data);
    //        repositoryMock.Verify(r => r.Add(It.IsAny<Book>()), Times.Once);
    //    }

    //    [Fact]
    //    public async Task UpdateAsync_ShouldReturnSuccessResult()
    //    {
    //        // Arrange
    //        var mapperMock = new Mock<IMapper>();
    //        var repositoryMock = new Mock<GenericRepository<Book>>();

    //        var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
    //        var bookDto = new BookDto(); // Assuming BookDto is a class implementing IDto

    //        // Act
    //        var result = bookService.UpdateAsync(bookDto);

    //        // Assert
    //        Assert.True(result.Success);
    //        Assert.Equal(bookDto, result.Data);
    //        repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
    //    }

    //    [Fact]
    //    public async Task DeleteAsync_ShouldReturnSuccessResult()
    //    {
    //        // Arrange
    //        var mapperMock = new Mock<IMapper>();
    //        var repositoryMock = new Mock<GenericRepository<Book>>();

    //        var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
    //        var bookId = 1;

    //        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Book()); // Assuming GetByIdAsync returns a Book entity

    //        // Act
    //        var result = await bookService.DeleteAsync(bookId);

    //        // Assert
    //        Assert.True(result.Success);
    //        Assert.Null(result.Data);
    //        repositoryMock.Verify(r => r.Update(It.IsAny<Book>()), Times.Once);
    //    }

    //    [Fact]
    //    public async Task GetAsync_ShouldReturnSuccessResult()
    //    {
    //        // Arrange
    //        var mapperMock = new Mock<IMapper>();
    //        var repositoryMock = new Mock<GenericRepository<Book>>();

    //        var bookService = new BookService(repositoryMock.Object, mapperMock.Object);
    //        var bookId = 1;

    //        repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Book()); // Assuming GetByIdAsync returns a Book entity

    //        // Act
    //        var result = await bookService.GetAsync(bookId);

    //        // Assert
    //        Assert.True(result.Success);
    //        Assert.NotNull(result.Data);
    //        mapperMock.Verify(m => m.Map<BookDto>(It.IsAny<Book>()), Times.Once);
    //    }

    //    [Fact]
    //    public void Success_ShouldReturnSuccessResult()
    //    {
    //        // Arrange
    //        var data = new BookDto();
    //        var expectedResult = new ServiceResult<BookDto> { Success = true, Data = data, Errors = new List<string>() };

    //        // Act
    //        var result = BaseService<Book, BookDto>.Success(data);

    //        // Assert
    //        Assert.Equal(expectedResult.Success, result.Success);
    //        Assert.Equal(expectedResult.Data, result.Data);
    //        Assert.Equal(expectedResult.Errors, result.Errors);
    //    }

    //    [Fact]
    //    public void FailureWithErrorMessage_ShouldReturnFailureResultWithErrorMessage()
    //    {
    //        // Arrange
    //        var errorMessage = "An error occurred.";
    //        var expectedResult = new ServiceResult<BookDto> { Success = false, Data = default, Errors = new List<string> { errorMessage } };

    //        // Act
    //        var result = BaseService<Book, BookDto>.Failure(errorMessage);

    //        // Assert
    //        Assert.Equal(expectedResult.Success, result.Success);
    //        Assert.Equal(expectedResult.Data, result.Data);
    //        Assert.Equal(expectedResult.Errors, result.Errors);
    //    }

    //    [Fact]
    //    public void FailureWithListOfErrorMessages_ShouldReturnFailureResultWithListOfErrorMessages()
    //    {
    //        // Arrange
    //        var errorMessages = new List<string> { "Error 1", "Error 2" };
    //        var expectedResult = new ServiceResult<BookDto> { Success = false, Data = default, Errors = errorMessages };

    //        // Act
    //        var result = BaseService<Book, BookDto>.Failure(errorMessages);

    //        // Assert
    //        Assert.Equal(expectedResult.Success, result.Success);
    //        Assert.Equal(expectedResult.Data, result.Data);
    //        Assert.Equal(expectedResult.Errors, result.Errors);
    //    }

    //    [Fact]
    //    public void Result_ShouldReturnResultWithGivenParameters()
    //    {
    //        // Arrange
    //        var data = new BookDto();
    //        var success = true;
    //        var errors = new List<string> { "Error 1", "Error 2" };
    //        var expectedResult = new ServiceResult<BookDto> { Success = success, Data = data, Errors = errors };

    //        // Act
    //        var result = BaseService<Book, BookDto>.Result(data, success, errors);

    //        // Assert
    //        Assert.Equal(expectedResult.Success, result.Success);
    //        Assert.Equal(expectedResult.Data, result.Data);
    //        Assert.Equal(expectedResult.Errors, result.Errors);
    //    }
    //}
}
