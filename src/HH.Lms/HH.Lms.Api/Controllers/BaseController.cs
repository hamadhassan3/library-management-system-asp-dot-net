using HH.Lms.Service;
using HH.Lms.Service.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HH.Lms.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private const string DefaultSuccessMessage = "Success";
        private const string DefaultFailMessage = "Operation failed.";
        private const string DefaultNotFoundMessage = "No record found.";
        private const string DefaultBadRequest = "Bad Request.";

        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }

        protected ILogger Logger { get; }

        protected ResponseDto Success(string message = null)
        {
            return Success<object>(null, message);
        }

        protected ResponseDto Fail(string message = null)
        {
            return Fail<object>(null, message);
        }

        protected ResponseDto SuccessOrFail(bool success, string message = null)
        {
            return success
                ? Success<object>(null, message)
                : Fail<object>(null, message);
        }

        protected ResponseDto<T> Success<T>(T data, string message = null)
        {
            return new ResponseDto<T>(data, true, message ?? DefaultSuccessMessage);
        }

        protected ResponseDto<T> SuccessOrNull<T>(T data, string message = null)
        {
            return data == null ? default : Success(data, message);
        }

        protected ResponseDto<T> Fail<T>(T data, string message = null)
        {
            return new ResponseDto<T>(data, false, message ?? DefaultFailMessage);
        }

        protected ResponseDto<T> NoResult<T>(string message = null, string error = null) where T : class
        {
            if (error != null)
                Logger.LogError(error);

            return new ResponseDto<T>(null, true, message ?? DefaultNotFoundMessage);
        }

        /// <summary>
        ///     Returns a Bad Request.
        ///     Use this when the Api input parameters are incorrect.
        /// </summary>
        protected ResponseDto<T> BadRequest<T>(string message = null, List<string> errors = null)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new ResponseDto<T> { Errors = errors, Message = message };
        }

        protected ResponseDto<T> Unauthorized<T>(string message = null) where T : class
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return new ResponseDto<T>(null, false, message);
        }

        protected ResponseDto<T> NotFound<T>(string message = null, string error = null)
        {
            if (error != null)
                Logger.LogError(error);

            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new ResponseDto<T>(default, false, message ?? DefaultNotFoundMessage);
        }

        protected ResponseDto<T> Result<T>(ServiceResult<T> result, bool notFoundOnEmpty = false)
        {
            if (notFoundOnEmpty && result.Data == null)
                return NotFound<T>();

            return result.Success ? Success(result.Data) : BadRequest<T>(DefaultBadRequest, result.Errors);
        }

        protected ResponseDto GetModelStateErrorResponse()
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return ResponseDto.Failure("Validation failed.", errors);
        }
    }
}
