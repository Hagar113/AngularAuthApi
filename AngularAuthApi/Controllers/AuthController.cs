using AngularAuthApi.Localization;
using DataAccess;
using DataProvider.IProvider;
using Infrastructure.helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Models.DTOs.BaseRequest;
using Models.DTOs.Request;
using Models.DTOs.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;
        private readonly IStringLocalizer _localizer;

        public AuthController(ApplicationDBContext context, IAuthProvider authProvider, IStringLocalizer<JsonStringLocalizer> localizer)
        {
            _authProvider = authProvider;
            _localizer = localizer;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<GeneralResponse>> Login([FromBody] BaseRequestHeader loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]));
            }

            var result = await _authProvider.AuthenticationRepo.Login(loginRequest);
            if (result == null)
            {
                return NotFound(GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["UserNotFound"]));
            }

            return Ok(GeneralResponse.Create(HttpStatusCode.OK, result, _localizer["LoginSuccessful"]));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<GeneralResponse>> Register([FromBody] BaseRequestHeader baseRequest)
        {
            if (baseRequest == null)
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]));
            }

            var registerRequest = Deserialization.Deserialize<RegisterRequest>(baseRequest.data.ToString());
            if (registerRequest == null)
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DeserializationFailed"]));
            }

            var validationResults = ValidateRegisterRequest(registerRequest);
            if (validationResults.Any())
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ValidationFailed"], validationResults));
            }

            if (await _authProvider.AuthenticationRepo.checkIfEmailOrPhoneOrUsernameExists(registerRequest.email, registerRequest.phone))
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["EmailOrPhoneExists"]));
            }

            var validation = _authProvider.AuthenticationRepo.CheckPasswordStrength(registerRequest.password);
            if (!string.IsNullOrEmpty(validation))
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, validation));
            }

            // Create BaseRequestHeader with serialized RegisterRequest
            var headerRequest = new BaseRequestHeader
            {
                userId = baseRequest.userId,
                languageCode = baseRequest.languageCode,
                data = JsonConvert.SerializeObject(registerRequest)
            };

            var registered = await _authProvider.AuthenticationRepo.UserRegister(headerRequest);
            if (registered)
            {
                return Ok(GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["RegistrationSuccessful"]));
            }
            else
            {
                return BadRequest(GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["RoleNotExist"]));
            }
        }



        private ICollection<string> ValidateRegisterRequest(RegisterRequest request)
        {
            var errors = new List<string>();
            var context = new ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(request, context, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                errors.AddRange(validationResult.MemberNames.Select(memberName => validationResult.ErrorMessage));
            }

            if (!new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$").IsMatch(request.email))
            {
                errors.Add(_localizer["InvalidEmail"]);
            }

            if (!new Regex(@"^(010|011|012)\d{8}$").IsMatch(request.phone))
            {
                errors.Add(_localizer["InvalidPhoneNumber"]);
            }

            if (!new Regex(@"^\d{4}(-\d{4})?$").IsMatch(request.academicYear))
            {
                errors.Add(_localizer["InvalidAcademicYear"]);
            }

            if (request.dateOfBirth != default)
            {
                var dateOfBirth = request.dateOfBirth;
                var calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
                {
                    calculatedAge--;
                }

                // Check if calculated age matches the age provided
                if (request.age != calculatedAge)
                {
                    errors.Add(_localizer["AgeMismatch"]);
                }

                // Role-based age validation
                if (request.RoleId == 1)
                {
                    if (calculatedAge < 14 || calculatedAge > 25)
                    {
                        errors.Add(_localizer["InvalidStudentAge"]);
                    }
                }
                else if (request.RoleId == 2)
                {
                    if (calculatedAge < 25 || calculatedAge > 60)
                    {
                        errors.Add(_localizer["InvalidTeacherAge"]);
                    }
                }
            }

            return errors;
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<GeneralResponse>> GetAllRoles()
        {
            try
            {
                var roles = await _authProvider.AuthenticationRepo.GetAllRoles();
                return Ok(GeneralResponse.Create(HttpStatusCode.OK, roles, _localizer["RolesRetrieved"]));
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"]));
            }
        }

        [HttpPost("validate-user-role")]
        public async Task<IActionResult> ValidateUserRole(userRequest request)
        {
            var response = await _authProvider.AuthenticationRepo.ValidateUserRole(request);

            if (response == null || !response.IsValid)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    isSuccess = false,
                    result = (object)null,
                    errors = new
                    {
                        message = _localizer["InvalidUserRole"]
                    }
                });
            }

            return Ok(new
            {
                statusCode = 200,
                isSuccess = true,
                result = response,
                errors = (object)null
            });
        }
    }
}
