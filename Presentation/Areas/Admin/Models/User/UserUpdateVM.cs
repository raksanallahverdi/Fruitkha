using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Admin.Models.User
{
    public class UserUpdateVM
    {
      

        [Required(ErrorMessage = "Must be entered!")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords didn't match")]
        public string? ConfirmNewPassword { get; set; }
       
    }
}
