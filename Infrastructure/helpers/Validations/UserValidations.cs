//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text.RegularExpressions;

//namespace Infrastructure.Helpers.Validations
//{
//    public class UserValidations : ValidationAttribute
//    {
//        private readonly string _validationType;

//        public UserValidations(string validationType)
//        {
//            _validationType = validationType;
//        }

//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            if (value == null || string.IsNullOrEmpty(value.ToString()))
//            {
//                return new ValidationResult($"{_validationType} is required");
//            }

//            // Retrieve the Role ID if it is available in the validation context
//            var roleIdProperty = validationContext.ObjectType.GetProperty("RoleId");
//            var roleId = roleIdProperty?.GetValue(validationContext.ObjectInstance, null) as int?;

//            var dateOfBirthProperty = validationContext.ObjectType.GetProperty("DateOfBirth");
//            var dateOfBirth = dateOfBirthProperty?.GetValue(validationContext.ObjectInstance, null) as DateTime?;

//            if (_validationType == "AcademicYear")
//            {
//                var academicYear = value.ToString();
//                var regex = new Regex(@"^\d{4}(-\d{4})?$");

//                if (!regex.IsMatch(academicYear))
//                {
//                    return new ValidationResult("Academic year must be in the format 'YYYY' or 'YYYY-YYYY'");
//                }
//            }
//            else if (_validationType == "PhoneNumber")
//            {
//                var phoneNumber = value.ToString();
//                var regex = new Regex(@"^(010|011|012)\d{8}$");

//                if (!regex.IsMatch(phoneNumber))
//                {
//                    return new ValidationResult("Phone number must start with 010, 011, or 012 and be exactly 11 digits long");
//                }
//            }
//            else if (_validationType == "Email")
//            {
//                var email = value.ToString();
//                var regex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");

//                if (!regex.IsMatch(email))
//                {
//                    return new ValidationResult("Invalid email address");
//                }
//            }
//            else if (_validationType == "Age")
//            {
//                if (dateOfBirth.HasValue)
//                {
//                    var calculatedAge = DateTime.Today.Year - dateOfBirth.Value.Year;
//                    if (dateOfBirth.Value > DateTime.Today.AddYears(-calculatedAge))
//                    {
//                        calculatedAge--;
//                    }

//                    var ageProvided = value as int?;
//                    if (ageProvided.HasValue && ageProvided.Value != calculatedAge)
//                    {
//                        return new ValidationResult("Age does not match Date of Birth");
//                    }

//                    // Check age based on role
//                    if (roleId.HasValue)
//                    {
//                        if (roleId.Value == 1) // Assuming 1 is for Student
//                        {
//                            if (ageProvided.Value < 14 || ageProvided.Value > 25)
//                            {
//                                return new ValidationResult("For students, age must be between 14 and 25");
//                            }
//                        }
//                        else if (roleId.Value == 2) // Assuming 2 is for Teacher
//                        {
//                            if (ageProvided.Value < 25 || ageProvided.Value > 60)
//                            {
//                                return new ValidationResult("For teachers, age must be between 25 and 60");
//                            }
//                        }
//                    }
//                }
//            }

//            return ValidationResult.Success;
//        }
//    }
//}
