using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HH.Lms.UnitTest.DataTests.Repositories;

public class BookRepositoryTest
{
    private readonly Mock<LibraryDBContext> mockDBContext;
    private readonly Mock<DbSet<Book>> mockBookSet;

    private BookRepository? bookRepository;

    public BookRepositoryTest()
    {
        mockDBContext = new Mock<LibraryDBContext>();
        mockBookSet = new Mock<DbSet<Book>>();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectEntity()
    {
        const int entityId = 1;
        var mockEntity = new Mock<Book>();
        mockBookSet.Setup(s => s.FindAsync(entityId)).ReturnsAsync(mockEntity.Object);
        mockDBContext.Setup(s => s.Set<Book>()).Returns(mockBookSet.Object);
        bookRepository = new BookRepository(mockDBContext.Object);

        var result = await bookRepository.GetByIdAsync(entityId);

        Assert.NotNull(result);
        Assert.Equal(mockEntity.Object, result);
    }

    [Fact]
    public void Add_ShouldAddEntityToDbSet()
    {
        var mockEntity = new Mock<Book>();

        mockDBContext.Setup(s => s.Set<Book>()).Returns(mockBookSet.Object);

        bookRepository = new BookRepository(mockDBContext.Object);

        bookRepository.Add(mockEntity.Object);

        mockBookSet.Verify(s => s.Add(mockEntity.Object), Times.Once);
    }

}
