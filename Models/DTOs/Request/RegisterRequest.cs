using System.ComponentModel.DataAnnotations;


namespace Models.DTOs.Request
{
    public class RegisterRequest
    {
        public int id { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        public string? userName { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        //[UserValidations("Email")]  
        public string? email { get; set; }

        //[Required(ErrorMessage = "Phone number is required")]
        //[UserValidations("PhoneNumber")]  
        public string? phone { get; set; }

        //[Required(ErrorMessage = "First name is required")]
        public string? firstName { get; set; }

        //[Required(ErrorMessage = "Age is required")]
        //[Range(20, 60, ErrorMessage = "Age must be between 20 and 60")]
        public int age { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }

        //[Required(ErrorMessage = "Role ID is required")]
        public int RoleId { get; set; }

        //[Required(ErrorMessage = "Academic year is required")]
        //[UserValidations("AcademicYear")]  
        public string academicYear { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
