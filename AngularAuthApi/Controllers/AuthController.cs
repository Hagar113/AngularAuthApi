using Infrastructure.helpers;
using DataAccess;
using DataProvider.IProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Models.DTOs.BaseRequest;
using Models.DTOs.Request;
using Models.DTOs.Response;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using Infrastructure;

namespace AngularAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;
        private readonly IStringLocalizer _localizer;

        public AuthController(ApplicationDBContext context, IAuthProvider authProvider, IStringLocalizer<AuthController> localizer)
        {
            _authProvider = authProvider;
            _localizer = localizer;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<GeneralResponse> Login([FromBody] BaseRequestHeader loginRequest)
        {
            if (loginRequest == null)
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]);
            }

            var result = await _authProvider.AuthenticationRepo.Login(loginRequest);
            if (result == null)
            {
                return GeneralResponse.Create(HttpStatusCode.NotFound, null, _localizer["UserNotFound"]);
            }

            return GeneralResponse.Create(HttpStatusCode.OK, result, _localizer["LoginSuccessful"]);
        }

        [HttpPost("Register")]
        public async Task<GeneralResponse> Register([FromBody] BaseRequestHeader baseRequest)
        {
            if (baseRequest == null)
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DataNotFound"]);
            }

            var registerRequest = Deserialization.Deserialize<RegisterRequest>(baseRequest.data.ToString());
            if (registerRequest == null)
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["DeserializationFailed"]);
            }

            var validationResults = ValidateRegisterRequest(registerRequest);
            if (validationResults.Any())
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["ValidationFailed"], validationResults);
            }

            if (await _authProvider.AuthenticationRepo.checkIfEmailOrPhoneOrUsernameExists(registerRequest.email, registerRequest.phone))
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["EmailOrPhoneExists"]);
            }

            var validation = _authProvider.AuthenticationRepo.CheckPasswordStrength(registerRequest.password);
            if (!string.IsNullOrEmpty(validation))
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, validation);
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
                return GeneralResponse.Create(HttpStatusCode.OK, null, _localizer["RegistrationSuccessful"]);
            }
            else
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["RoleNotExist"]);
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
                        errors.Add(_localizer["InvalidAdminAge"]);
                    }
                }
                else if (request.RoleId == 3)
                {
                    if (calculatedAge < 25 || calculatedAge > 60)
                    {
                        errors.Add(_localizer["InvalidTeacherAge"]); // Use the same error message as for RoleId 2 or create a new one if needed
                    }
                }
            }


            return errors;
        }

        [HttpGet("GetAllRoles")]
        public async Task<GeneralResponse> GetAllRoles()
        {
            try
            {
                var roles = await _authProvider.AuthenticationRepo.GetAllRoles();
                return GeneralResponse.Create(HttpStatusCode.OK, roles, _localizer["RolesRetrieved"]);
            }
            catch
            {
                return GeneralResponse.Create(HttpStatusCode.InternalServerError, null, _localizer["ErrorOccurred"]);
            }
        }

        [HttpPost("validate-user-role")]
        public async Task<GeneralResponse> ValidateUserRole(userRequest request)
        {
            var response = await _authProvider.AuthenticationRepo.ValidateUserRole(request);

            if (response == null || !response.IsValid)
            {
                return GeneralResponse.Create(HttpStatusCode.BadRequest, null, _localizer["InvalidUserRole"]);
            }

            return GeneralResponse.Create(HttpStatusCode.OK, response, null);
        }
    }
}
