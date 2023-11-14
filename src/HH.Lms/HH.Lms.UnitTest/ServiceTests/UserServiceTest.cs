using AutoMapper;
using HH.Lms.Data.Library.Entities;
using HH.Lms.Data.Repository.EntityRepository;
using HH.Lms.Service.Library.Dto;
using HH.Lms.Service.Library;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HH.Lms.UnitTest.ServiceTests
{
    public class UserServiceTest
    {
        [Fact]
        public async Task AddAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<UserRepository>();
            var userMock = new Mock<User>();

            var userDto = new UserDto();

            repositoryMock.Setup((r) => r.Add(It.IsAny<User>())).Callback(() => {
                // Does nothing
            });
            mapperMock.Setup((s) => s.Map<User>(userDto)).Returns(userMock.Object);
            var userService = new UserService(repositoryMock.Object, mapperMock.Object);

            var result = await userService.AddAsync(userDto);

            Assert.True(result.Success);
            repositoryMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<UserRepository>();
            var userMock = new Mock<User>();

            repositoryMock.Setup((r) => r.Update(It.IsAny<User>())).Callback(() => {
                // Does nothing
            });
            mapperMock.Setup((s) => s.Map<User>(It.IsAny<UserDto>())).Returns(userMock.Object);
            var userService = new UserService(repositoryMock.Object, mapperMock.Object);
            var userDto = new UserDto();

            var result = await userService.UpdateAsync(userDto);

            Assert.True(result.Success);
            Assert.Equal(userDto, result.Data);
            repositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<UserRepository>();
            var userMock = new Mock<User>();

            repositoryMock.Setup((r) => r.Delete(It.IsAny<User>())).Callback(() => {
                // Does nothing
            });
            mapperMock.Setup((s) => s.Map<User>(It.IsAny<UserDto>())).Returns(userMock.Object);
            var userService = new UserService(repositoryMock.Object, mapperMock.Object);
            var userId = 1;

            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new User());

            await userService.DeleteAsync(userId);

        }

        [Fact]
        public async Task GetAsync_ShouldReturnSuccessResult()
        {
            var mapperMock = new Mock<IMapper>();
            var repositoryMock = new Mock<UserRepository>();
            var userMock = new Mock<User>();
            var userDto = new Mock<UserDto>();

            mapperMock.Setup((s) => s.Map<UserDto>(It.IsAny<User>())).Returns(userDto.Object);
            var userService = new UserService(repositoryMock.Object, mapperMock.Object);
            var userId = 1;

            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(userMock.Object);

            var result = await userService.GetAsync(userId);

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            mapperMock.Verify(m => m.Map<UserDto>(It.IsAny<User>()), Times.Once);
        }

    }
}
