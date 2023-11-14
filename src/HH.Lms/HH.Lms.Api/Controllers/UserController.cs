using HH.Lms.Service;
using HH.Lms.Service.Dto;
using HH.Lms.Service.Library;
using HH.Lms.Service.Library.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HH.Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService userService;

        public UserController(ILogger<UserController> logger, UserService userService) : base(logger)
        {
            this.userService = userService;
        }

        /// <summary>
        /// This method gets all the users stored in the system.
        /// </summary>
        /// <returns> The list of users. </returns>
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ResponseDto<IEnumerable<UserDto>>> GetAllUsers()
        {
            ServiceResult<IEnumerable<UserDto>> users = await userService.GetAllAsync();
            return Result(users);
        }

        /// <summary>
        /// This method gets a user using its id.
        /// </summary>
        /// <returns> The User. </returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ResponseDto<UserDto>> GetUserById(int id)
        {
            ServiceResult<UserDto> user = await userService.GetAsync(id);
            return Result(user);
        }

        /// <summary>
        /// This method gets a user using its id after eager loading the books.
        /// </summary>
        /// <returns> The User. </returns>
        [AllowAnonymous]
        [HttpGet("{id}/books")]
        public async Task<ResponseDto<UserDto>> GetUserByIdWithBooks(int id)
        {
            ServiceResult<UserDto> user = await userService.loadBooks(id);
            return Result(user);
        }

        /// <summary>
        /// Creates a user using the data in payload.
        /// </summary>
        /// <returns> The created user. </returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResponseDto<UserDto>> CreateUser([FromBody] UserDto userDto)
        {
            ServiceResult<UserDto> user = await userService.AddAsync(userDto);
            return Result(user);
        }

        /// <summary>
        /// Updates a user using the data in payload.
        /// </summary>
        /// <returns> The updated user. </returns>
        [AllowAnonymous]
        [HttpPut]
        public async Task<ResponseDto<UserDto>> UpdateUser([FromBody] UserDto userDto)
        {
            ServiceResult<UserDto> user = await userService.UpdateAsync(userDto);
            return Result(user);
        }

        /// <summary>
        /// Deletes a user using its id.
        /// </summary>
        /// <returns> Success if the user is deleted. </returns>
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ResponseDto<string>> DeleteUser(int id)
        {
            await userService.DeleteAsync(new UserDto { Id = id });
            return new ResponseDto<string>("Successfully Deleted.", true, "Success!");
        }
    }
}
