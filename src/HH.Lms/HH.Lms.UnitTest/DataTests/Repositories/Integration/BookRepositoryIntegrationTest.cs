using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HH.Lms.UnitTest.DataTests.Repositories.Integration;

public class BookRepositoryIntegrationTests : IDisposable
{
    private readonly DbContextOptions<LibraryDBContext> dbContextOptions;
    private readonly LibraryDBContext dbContext;
    private readonly Book testBook1;
    private readonly Book testBook2;

    public BookRepositoryIntegrationTests()
    {
        dbContextOptions = new DbContextOptionsBuilder<LibraryDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new LibraryDBContext(dbContextOptions);

        testBook1 = new Book
        {
            Id = 1,
            Author = "Test Author1",
            Title = "Test Book1",
            Isbn = "Test ISBN1",
            Description = "Description1"

        };

        testBook2 = new Book
        {
            Id = 2,
            Author = "Test Author2",
            Title = "Test Book2",
            Isbn = "Test ISBN2",
            Description = "Description2"

        };
    }

    [Fact]
    public async Task Add_GetByIdAsync_ShouldAddBookToDatabase()
    {
        var repository = new BookRepository(dbContext);
        
        repository.Add(testBook1);
        await repository.SaveChangesAsync();

        var addedBook = await repository.GetByIdAsync(testBook1.Id);
        Assert.NotNull(addedBook);
        Assert.Equal(testBook1.Title, addedBook.Title);
        Assert.Equal(testBook1.Author, addedBook.Author);
        Assert.Equal(testBook1.Isbn, addedBook.Isbn);
        Assert.Equal(testBook1.Description, addedBook.Description);

        //Cleaning up
        repository.Delete(testBook1);
        await repository.SaveChangesAsync();

    }

    [Fact]
    public async Task Add_GetAllAsync_ShouldReturnAllBooks()
    {
        var repository = new BookRepository(dbContext);

        repository.Add(testBook1);
        repository.Add(testBook2);
        await repository.SaveChangesAsync();

        var booksEnumerable = await repository.GetAllAsync();
        Assert.NotNull(booksEnumerable);

        var booksList = booksEnumerable.ToList();
        Assert.Equal(2, booksList.Count());

        Assert.Equal(testBook1.Title, booksList[0].Title);
        Assert.Equal(testBook1.Author, booksList[0].Author);
        Assert.Equal(testBook1.Isbn, booksList[0].Isbn);
        Assert.Equal(testBook1.Description, booksList[0].Description);

        Assert.Equal(testBook2.Title, booksList[1].Title);
        Assert.Equal(testBook2.Author, booksList[1].Author);
        Assert.Equal(testBook2.Isbn, booksList[1].Isbn);
        Assert.Equal(testBook2.Description, booksList[1].Description);

        //Cleaning up
        repository.Delete(testBook1);
        repository.Delete(testBook2);
        await repository.SaveChangesAsync();
    }

    [Fact]
    public async Task Add_Update_GetByIdAsync_TheBookShouldBeAddedAndUpdated()
    {
        var repository = new BookRepository(dbContext);

        repository.Add(testBook1);
        await repository.SaveChangesAsync();

        testBook1.Author = testBook2.Author;
        testBook1.Title = testBook2.Title;
        testBook1.Isbn = testBook2.Isbn;
        testBook1.Description = testBook2.Description;

        repository.Update(testBook1);
        await repository.SaveChangesAsync();

        var updatedBook = await repository.GetByIdAsync(testBook1.Id);
        Assert.NotNull(updatedBook);
        Assert.Equal(testBook2.Title, updatedBook.Title);
        Assert.Equal(testBook2.Author, updatedBook.Author);
        Assert.Equal(testBook2.Isbn, updatedBook.Isbn);
        Assert.Equal(testBook2.Description, updatedBook.Description);

        //Cleaning up
        repository.Delete(testBook1);
        await repository.SaveChangesAsync();

    }

    [Fact]
    public async Task Add_Delete_GetByIdAsync_TheBookShouldNotExistAfterDelete()
    {
        var repository = new BookRepository(dbContext);

        repository.Add(testBook1);
        await repository.SaveChangesAsync();

        repository.Delete(testBook1);
        await repository.SaveChangesAsync();

        var result = await repository.GetByIdAsync(testBook1.Id);
        Assert.Null(result);

    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
