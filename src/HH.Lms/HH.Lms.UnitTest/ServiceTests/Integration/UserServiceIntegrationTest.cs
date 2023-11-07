using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Library;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Library.Dto;
using HH.Lms.Service.Library;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HH.Lms.UnitTest.ServiceTests.Integration
{
    public class UserServiceIntegrationTest
    {
        private readonly DbContextOptions<LibraryDBContext> dbContextOptions;
        private readonly LibraryDBContext dbContext;
        private readonly UserDto testUser1;
        private readonly UserDto testUser2;
        private readonly UserDto newUser;
        private readonly IMapper mapper;
        private readonly UserService userService;
        private readonly UserDto updatedUser;

        public UserServiceIntegrationTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<LibraryDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new LibraryDBContext(dbContextOptions);

            testUser1 = new UserDto
            {
                Id = 1,
                FirstName = "Fname1",
                LastName = "Lname1",
                Role = "Role1"

            };

            testUser2 = new UserDto
            {
                Id = 2,
                FirstName = "Fname2",
                LastName = "Lname2",
                Role = "Role2"

            };

            newUser = new UserDto
            {
                Id = 10,
                FirstName = "Fname New",
                LastName = "Lname New",
                Role = "Role New"
            };
            var newUserObj = new User
            {
                Id = 10,
                FirstName = "Fname New",
                LastName = "Lname New",
                Role = "Role New"
            };

            updatedUser = new UserDto
            {
                Id = testUser1.Id,
                FirstName = "Fname Updated",
                LastName = "Lname Updated",
                Role = "Role Updated"
            };

            var updatedUserObj = new User
            {
                Id = testUser1.Id,
                FirstName = "Fname Updated",
                LastName = "Lname Updated",
                Role = "Role Updated"
            };

            var users = new List<User>
            {
                new User
                    {
                        Id = 1,
                        FirstName = "Fname1",
                        LastName = "Lname1",
                        Role = "Role1"
                    },
                new User
                    {
                        Id = 2,
                        FirstName = "Fname2",
                        LastName = "Lname2",
                        Role = "Role2"
                    }
            };

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup((m) => m.Map<UserDto>(users[0])).Returns(testUser1);
            mapperMock.Setup((m) => m.Map<UserDto>(users[1])).Returns(testUser2);
            mapperMock.Setup((m) => m.Map<UserDto>(newUserObj)).Returns(newUser);
            mapperMock.Setup((m) => m.Map<UserDto>(updatedUserObj)).Returns(updatedUser);

            mapperMock.Setup((m) => m.Map<User>(testUser1)).Returns(users[0]);

            mapperMock.Setup((m) => m.Map<User>(testUser2)).Returns(users[1]);

            mapperMock.Setup((m) => m.Map<User>(newUser)).Returns(newUserObj);
            mapperMock.Setup((m) => m.Map<User>(updatedUser)).Returns(updatedUserObj);


            mapperMock.Setup((m) => m.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>())).Returns(new List<UserDto> { testUser1, testUser2 }.AsEnumerable());

            mapper = mapperMock.Object;

            userService = new UserService(new UserRepository(dbContext), mapper);
        }

        [Fact]
        public async void GetAllUsers_ShouldReturnAllUsers()
        {
            // Adding users
            await userService.AddAsync(testUser1);
            await userService.AddAsync(testUser2);

            var result = await userService.GetAllAsync();

            var ls = result.Data.ToList();

            Assert.NotNull(result);
            Assert.IsType<List<UserDto>>(ls);
            Assert.Equal(2, ls.Count);
            Assert.Contains(ls, b => b.Id == testUser1.Id);
            Assert.Contains(ls, b => b.Id == testUser2.Id);

            // Cleaning up
            await userService.DeleteAsync(testUser1.Id);
            await userService.DeleteAsync(testUser2.Id);
        }

        [Fact]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            var result = userService.GetAsync(testUser1.Id);

            Assert.NotNull(result);
            Assert.Equal(testUser1.Id, result.Id);
        }

        [Fact]
        public async void AddUser_ShouldAddNewUser()
        {

            var addedUser = await userService.AddAsync(newUser);

            Assert.NotNull(addedUser);
            Assert.Equal(newUser.FirstName, addedUser.Data.FirstName);
            Assert.Equal(newUser.LastName, addedUser.Data.LastName);

            var retrievedUser = dbContext.Users.Find(addedUser.Data.Id);
            Assert.NotNull(retrievedUser);
            Assert.Equal(newUser.FirstName, retrievedUser.FirstName);
            Assert.Equal(newUser.LastName, retrievedUser.LastName);
        }

        [Fact]
        public async void UpdateUser_ShouldUpdateExistingUser()
        {
            var res = await userService.AddAsync(testUser1);

            UserDto addedUser = res.Data;

            addedUser.FirstName = updatedUser.FirstName;
            addedUser.LastName = updatedUser.LastName;

            var result = await userService.UpdateAsync(addedUser);

            Assert.NotNull(result);
            Assert.Equal(updatedUser.FirstName, result.Data.FirstName);
            Assert.Equal(updatedUser.LastName, result.Data.LastName);

            var retrievedUser = await userService.GetAsync(result.Data.Id);
            Assert.NotNull(retrievedUser);
            Assert.Equal(updatedUser.FirstName, retrievedUser.Data.FirstName);
            Assert.Equal(updatedUser.LastName, retrievedUser.Data.LastName);
        }
    }
}
