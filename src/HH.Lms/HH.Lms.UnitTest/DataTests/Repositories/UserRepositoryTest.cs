using HH.Lms.Data.Library;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HH.Lms.UnitTest.DataTests.Repositories
{
    public class UserRepositoryTest
    {
        private readonly Mock<LibraryDBContext> mockDBContext;
        private readonly Mock<DbSet<User>> mockUserSet;

        private UserRepository? userRepository;

        public UserRepositoryTest()
        {
            mockDBContext = new Mock<LibraryDBContext>();
            mockUserSet = new Mock<DbSet<User>>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectEntity_WhenEntityExists()
        {
            const int entityId = 1;
            var mockEntity = new Mock<User>();
            mockUserSet.Setup(s => s.FindAsync(entityId)).ReturnsAsync(mockEntity.Object);
            mockDBContext.Setup(s => s.Set<User>()).Returns(mockUserSet.Object);
            userRepository = new UserRepository(mockDBContext.Object);

            var result = await userRepository.GetByIdAsync(entityId);

            Assert.NotNull(result);
            Assert.Equal(mockEntity.Object, result);
        }

        [Fact]
        public void Add_ShouldAddEntityToDbSet()
        {
            var mockEntity = new Mock<User>();

            mockDBContext.Setup(s => s.Set<User>()).Returns(mockUserSet.Object);

            userRepository = new UserRepository(mockDBContext.Object);

            userRepository.Add(mockEntity.Object);

            mockUserSet.Verify(s => s.Add(mockEntity.Object), Times.Once);
        }
    }
}
