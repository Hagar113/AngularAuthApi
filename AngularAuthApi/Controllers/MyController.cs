using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            if (HttpContext.Items.TryGetValue("Localizer", out var localizerObj) && localizerObj is IStringLocalizer localizer)
            {
                var errorCodes = new List<string> { "InvalidEmail", "InvalidPhoneNumber", "InvalidAcademicYear", "AgeMismatch" };
                var errorMessages = errorCodes.Select(code => localizer[code].Value).ToList();

                var response = new
                {
                    statusCode = 400,
                    requestId = Guid.NewGuid().ToString(),
                    error = errorMessages,
                    result = (object)null,
                    success = false,
                    responseMessage = localizer["ValidationFailed"].Value
                };

                return BadRequest(response);
            }

            return StatusCode(500, "Localization failed");
        }
    }
}
