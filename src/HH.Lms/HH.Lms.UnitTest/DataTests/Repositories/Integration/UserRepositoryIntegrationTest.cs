using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HH.Lms.UnitTest.DataTests.Repositories.Integration;

public class UserRepositoryIntegrationTest : IDisposable
{
    private readonly DbContextOptions<LibraryDBContext> dbContextOptions;
    private readonly LibraryDBContext dbContext;
    private readonly User testUser1;
    private readonly User testUser2;

    public UserRepositoryIntegrationTest()
    {
        dbContextOptions = new DbContextOptionsBuilder<LibraryDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        dbContext = new LibraryDBContext(dbContextOptions);

        testUser1 = new User
        {
            Id = 1,
            FirstName = "FirstName Test1",
            LastName = "LastName Test1",
            Role = "Test1"
        };

        testUser2 = new User
        {
            Id = 2,
            FirstName = "FirstName Test2",
            LastName = "LastName Test2",
            Role = "Tes2"
        };
    }

    [Fact]
    public async Task Add_GetByIdAsync_ShouldAddUserToDatabase()
    {
        var repository = new UserRepository(dbContext);

        repository.Add(testUser1);
        await repository.SaveChangesAsync();

        var addedUser = await repository.GetByIdAsync(testUser1.Id);

        Assert.NotNull(addedUser);
        Assert.Equal(testUser1.FirstName, addedUser.FirstName);
        Assert.Equal(testUser1.LastName, addedUser.LastName);
        Assert.Equal(testUser1.Role, addedUser.Role);

        // Cleaning up
        repository.Delete(testUser1);
        await repository.SaveChangesAsync();
    }

    [Fact]
    public async Task Add_GetAllAsync_ShouldReturnAllUsers()
    {
        var repository = new UserRepository(dbContext);

        repository.Add(testUser1);
        repository.Add(testUser2);
        await repository.SaveChangesAsync();

        var usersEnumerable = await repository.GetAllAsync();
        Assert.NotNull(usersEnumerable);

        var usersList = usersEnumerable.ToList();
        Assert.Equal(2, usersList.Count());

        Assert.Equal(testUser1.FirstName, usersList[0].FirstName);
        Assert.Equal(testUser1.LastName, usersList[0].LastName);
        Assert.Equal(testUser1.Role, usersList[0].Role);

        Assert.Equal(testUser2.FirstName, usersList[1].FirstName);
        Assert.Equal(testUser2.LastName, usersList[1].LastName);
        Assert.Equal(testUser2.Role, usersList[1].Role);

        // Cleaning up
        repository.Delete(testUser1);
        repository.Delete(testUser2);
        await repository.SaveChangesAsync();
    }

    [Fact]
    public async Task Add_Update_GetByIdAsync_TheUserShouldBeAddedAndUpdated()
    {
        var repository = new UserRepository(dbContext);

        repository.Add(testUser1);
        await repository.SaveChangesAsync();

        testUser1.FirstName = testUser2.FirstName;
        testUser1.LastName = testUser2.LastName;
        testUser1.Role = testUser2.Role;

        repository.Update(testUser1);
        await repository.SaveChangesAsync();

        var updatedUser = await repository.GetByIdAsync(testUser1.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal(testUser2.FirstName, updatedUser.FirstName);
        Assert.Equal(testUser2.LastName, updatedUser.LastName);
        Assert.Equal(testUser2.Role, updatedUser.Role);

        // Cleaning up
        repository.Delete(testUser1);
        await repository.SaveChangesAsync();
    }

    [Fact]
    public async Task Add_Delete_GetByIdAsync_TheUserShouldNotExistAfterDelete()
    {
        var repository = new UserRepository(dbContext);

        repository.Add(testUser1);
        await repository.SaveChangesAsync();

        repository.Delete(testUser1);
        await repository.SaveChangesAsync();

        var result = await repository.GetByIdAsync(testUser1.Id);
        Assert.Null(result);

        Dispose();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
