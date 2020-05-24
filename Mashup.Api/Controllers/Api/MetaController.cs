using Mashup.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Mashup.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ApiControllerBase
    {
        private int GetStatusCode(Exception error)
        {
            int statusCode;

            if (error is HttpResponseException)
            {
                statusCode = (int)(error as HttpResponseException).StatusCode;
            }
            else if (error is ValidationException)
            {
                statusCode = 400;
            }
            else
            {
                statusCode = 500;
            }

            return statusCode;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = GetStatusCode(context.Error);

            return Problem(
                statusCode: statusCode,
                title: context.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult ErrorDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException("Not a development environment");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = GetStatusCode(context.Error);

            return Problem(
                detail: context.Error.StackTrace,
                statusCode: statusCode,
                title: context.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error/{statusCode:int}")]
        public IActionResult ErrorStatusCode(int statusCode)
        {
            var errorMessages = new Dictionary<int, string>
            {
                { 400, "Bad Request. Check the given parameters." },
                { 401, "Unauthorized." },
                { 403, "Forbidden." },
                { 404, "Not Found. Check the URL of the endpoint." },
                { 405, "Method Not Allowed." },
                { 410, "Gone. These are not the Droids you're looking for. *Waves hand.*" },
                { 418, "I'm a teapot." },
                { 500, "Internal Server Error. More details can be found in the log." },
                { 501, "Not Implemented." },
                { 503, "Service Unavailable." },
            };
            if (!errorMessages.ContainsKey(statusCode))
            {
                statusCode = 500;
            }
            return Problem(
                statusCode: statusCode,
                title: errorMessages[statusCode]);
        }
    }
}
