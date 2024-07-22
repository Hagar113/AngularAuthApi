using Models.DTOs.Request;
using Models.DTOs.Response;
using Models.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepo
{
    public interface IAuthenticationRepo
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<bool> UserRegister(RegisterRequest registerRequest);
        Task<bool> checkIfEmailOrPhoneOrUsernameExists(string _email, string _phone);
        string CheckPasswordStrength(string _password);
        bool CheckRequestedObj(RegisterRequest registerRequest);
        //string GenerateJwtToken(Users user);

        Task<List<RolesResponse>> GetAllRoles();
        Task<Users> GetUserById(int userId);
        Task<bool> IsRoleAssignedToUser(int userId, string roleCode);
        Task<PagesResponse> ValidateUserRole(userRequest request);

    }

}
