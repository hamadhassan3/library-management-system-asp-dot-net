using HH.Lms.Service;
using HH.Lms.Service.Library;
using HH.Lms.Service.Library.Dto;
using HH.Lms.Service.Library.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace HH.Lms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : BaseController
{

    private UserService userService;
    private IConfiguration configuration;

    public AuthenticationController(UserService userService, IConfiguration configuration,
        ILogger<AuthenticationController> logger) : base(logger)
    {
        this.userService = userService;
        this.configuration = configuration;
    }

    /// <summary>
    /// Issues a Jwt token to the user. Doesn't check password as its just for testing.
    /// </summary>
    /// <returns>The jwt token.</returns>
    [AllowAnonymous]
    [HttpPost("{id}")]
    public async Task<ServiceResult<string>> GenerateToken(int id)
    {
        ServiceResult<UserDto> user = await userService.GetAsync(id);

        if (user  == null)
        {
            return new ServiceResult<string> { Success = false, Data = "Failed. A user with this id does not exist." };
        }

        string secret = configuration.GetValue<string>("JWTSecret")!;

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Data.Role)  // For simplicity, one user has only one role
                }
            ),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration.GetValue<string>("JWTIssuer")!,
            Audience = configuration.GetValue<string>("JWTAudience")!
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new ServiceResult<string> { Success = true, Data = tokenString };
    }
}
