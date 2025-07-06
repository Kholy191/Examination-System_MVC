using System.ComponentModel.DataAnnotations;
using Shared.Enums;

namespace WebExaminationApplication.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword {  get; set; } = string.Empty;
        public UserTypeEnum UserType { get; set; }
    }
}
