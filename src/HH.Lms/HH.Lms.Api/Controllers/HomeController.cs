using HH.Lms.Service.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HH.Lms.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Tests if the server is up.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("")]
        public ResponseDto<String> Up()
        {
            return new ResponseDto<string>(result: "Server is up!", success: true, message: "Success.");
        }
    }
}
