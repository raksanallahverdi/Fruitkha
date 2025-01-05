using System.ComponentModel.DataAnnotations;

namespace Business.Models.Account
{
    public class AccountForgetPasswordVM
    {
        [Required(ErrorMessage ="Mail Must be Entered!")]
        public string Email { get; set; }
    }
}
