using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUDExample.Models.Account
{
    public class LoginViewModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "The password must be {2} at least {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
