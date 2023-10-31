using HH.Lms.Service.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
