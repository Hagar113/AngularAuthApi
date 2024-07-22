
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.models
{
    public class Users : Defaults
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid email address")]
        //[UserValidations("Email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Phone number is required")]
        //[UserValidations("PhoneNumber")]
        public string phone { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Age is required")]
        //[Range(20, 60, ErrorMessage = "Age must be between 20 and 60")]
        //[UserValidations("Age")] // Ensure Age matches DateOfBirth and role
        public int Age { get; set; }

        //[Required(ErrorMessage = "School year is required")]
        //[UserValidations("AcademicYear")]
        public string schoolYear { get; set; }
        public string? Token { get; set; }

        //[Required(ErrorMessage = "Date of Birth is required")]
        //[DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime DateOfBirth { get; set; }

        //[Required(ErrorMessage = "Role ID is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Role ID must be a positive integer greater than 0")]
        [ForeignKey("Role")]
        public int? roleId { get; set; }
        public Roles Role { get; set; }
        public bool? isActive { get; set; }
    }
}
