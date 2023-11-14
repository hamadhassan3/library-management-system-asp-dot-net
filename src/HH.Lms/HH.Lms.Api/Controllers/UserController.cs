using FluentValidation.Results;
using HH.Lms.Service;
using HH.Lms.Service.Dto;
using HH.Lms.Service.Library;
using HH.Lms.Service.Library.Dto;
using HH.Lms.Service.Library.Validator;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin,User")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ResponseDto<UserDto>> CreateUser([FromBody] UserDto userDto)
        {
            var validator = new UserDtoValidator();

            ValidationResult result = validator.Validate(userDto);

            if (result.IsValid)
            {
                ServiceResult<UserDto> user = await userService.AddAsync(userDto);
                return Result(user);
            }
            else
            {
                return new ResponseDto<UserDto> { Success = false, Message = "Invalid Dto!", Errors = result.Errors.Select(e => e.ErrorMessage).ToList() };
            }
        }

        /// <summary>
        /// Updates a user using the data in payload.
        /// </summary>
        /// <returns> The updated user. </returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ResponseDto<UserDto>> UpdateUser([FromBody] UserDto userDto)
        {
            var validator = new UserDtoValidator();

            ValidationResult result = validator.Validate(userDto);

            if (result.IsValid)
            {
                ServiceResult<UserDto> user = await userService.UpdateAsync(userDto);
                return Result(user);
            }
            else
            {
                return new ResponseDto<UserDto> { Success = false, Message = "Invalid Dto!", Errors = result.Errors.Select(e => e.ErrorMessage).ToList() };
            }
        }

        /// <summary>
        /// Deletes a user using its id.
        /// </summary>
        /// <returns> Success if the user is deleted. </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ResponseDto<string>> DeleteUser(int id)
        {
            await userService.DeleteAsync(new UserDto { Id = id });
            return new ResponseDto<string>("Successfully Deleted.", true, "Success!");
        }
    }
}
