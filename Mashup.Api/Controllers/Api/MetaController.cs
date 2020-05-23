using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mashup.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ApiControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error() => Problem();

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException("Not a development environment");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error/{statusCode:int}")]
        public IActionResult ErrorCode(int statusCode)
        {
            var errorMessages = new Dictionary<int, string>
            {
                { 400, "Bad Request. Check the given parameters." },
                { 401, "Unauthorized." },
                { 403, "Forbidden." },
                { 404, "Not Found. Check the URL of the endpoint." },
                { 405, "Method Not Allowed." },
                { 410, "Gone. These Are Not the Droids You Are Looking For." },
                { 418, "I'm a teapot." },
                { 500, "Internal Server Error. More details can be found in the log." },
                { 501, "Not Implemented." },
                { 503, "Service Unavailable." },
            };
            if (!errorMessages.ContainsKey(statusCode))
            {
                statusCode = 500;
            }
            var objectValue = new
            {
                Message = errorMessages[statusCode]
            };
            var objectResult = new ObjectResult(objectValue)
            {
                StatusCode = statusCode
            };
            return objectResult;
        }
    }
}
