using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TranTriTaiBlog.DTOs.Responses;

namespace TranTriTaiBlog.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Error()
        {
            IExceptionHandlerFeature? context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            _logger.LogError(context?.Error, "Unexpected Error");

            return StatusCode(StatusCodes.Status500InternalServerError,
                new CommonResponse<string>(StatusCodes.Status500InternalServerError,
                "Internal server error! Please contact support team"));
        }
    }
}

